using Microsoft.AspNetCore.Mvc;
using TelephoneDirApi.DAL.AuthRepository;
using TelephoneDirApi.DTOs;
using TelephoneDirApi.Model;
using TelephoneDirApi.Services;


namespace TelephoneDirApi.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        private readonly IJwtService _jwtService;

        public AuthController(IAuthRepository authRepo, IJwtService jwtService)
        {
            _authRepo = authRepo;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] LoginDTO userDto)
        {
            var user = new UserModel
            {
                UserName = userDto.UserName,
                Email = userDto.Email,
                Password = userDto.Password,
                Role = userDto.Role
            };
            var result = await _authRepo.RegisterUser(user);
            if (!result)
                return BadRequest(new { message = "User already exists." });

            return Ok(new { message = "User registered successfully." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO userDto)
        {
            var existingUser = await _authRepo.LoginUser(userDto.Email);
            if (existingUser == null || !BCrypt.Net.BCrypt.Verify(userDto.Password, existingUser.Password))
            {
                return Unauthorized(new { message = "Invalid credentials." });
            }

            var accessToken = _jwtService.GenerateToken(existingUser);
            var refreshToken = _jwtService.GenerateRefreshToken();

            await _authRepo.SaveRefreshToken(existingUser.UserID, refreshToken, DateTime.UtcNow.AddDays(7));

            return Ok(new { AccessToken = accessToken, RefreshToken = refreshToken });
        }

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenDTO refreshTokenDto)
        {
            var user = await _authRepo.GetUserByRefreshToken(refreshTokenDto.RefreshToken);

            if (user == null || user.RefreshTokenExpiry < DateTime.UtcNow)
                return Unauthorized(new { message = "Invalid or expired refresh token" });

            var newAccessToken = _jwtService.GenerateToken(user);
            var newRefreshToken = _jwtService.GenerateRefreshToken();

            await _authRepo.SaveRefreshToken(user.UserID, newRefreshToken, DateTime.UtcNow.AddDays(7));

            return Ok(new { AccessToken = newAccessToken, RefreshToken = newRefreshToken });
        }


    }
}
