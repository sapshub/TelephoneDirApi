using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TelephoneDirApi.Model;

namespace TelephoneDirApi.Services
{
    public interface IJwtService
    {
        string GenerateToken(UserModel user);
        string GenerateRefreshToken();
    }
}
