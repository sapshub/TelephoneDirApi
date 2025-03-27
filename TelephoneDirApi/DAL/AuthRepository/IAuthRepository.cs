using TelephoneDirApi.Model;

namespace TelephoneDirApi.DAL.AuthRepository
{
    public interface IAuthRepository
    {
        Task<bool> RegisterUser(UserModel user);
        Task<UserModel> LoginUser(string email);
        Task<bool> SaveRefreshToken(int userId, string refreshToken, DateTime expiry);
        Task<UserModel?> GetUserByRefreshToken(string refreshToken);
    }
}
