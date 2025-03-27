using Microsoft.Data.SqlClient;
using System.Data;
using TelephoneDirApi.DAL.BaseRepositoryy;
using TelephoneDirApi.Model;

namespace TelephoneDirApi.DAL.AuthRepository
{
    public class AuthRepository : BaseRepository, IAuthRepository
    {
        public AuthRepository(IConfiguration configuration) : base(configuration) { }

        public async Task<bool> RegisterUser(UserModel user)
        {
            using (var conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("RegisterUser", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@UserName", user.UserName);
                cmd.Parameters.AddWithValue("@Email", user.Email);
                cmd.Parameters.AddWithValue("@PasswordHash", BCrypt.Net.BCrypt.HashPassword(user.Password));

                conn.Open();
                try
                {
                    await cmd.ExecuteNonQueryAsync();
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public async Task<UserModel> LoginUser(string email)
        {
            using (var conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("LoginUser", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Email", email);

                conn.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                if (reader.Read())
                {
                    return new UserModel
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        UserName = reader["UserName"].ToString(),
                        Email = reader["Email"].ToString(),
                        Password = reader["PasswordHash"].ToString(),
                        Role = reader["Role"].ToString()
                    };
                }
                return null;
            }
        }

        public async Task<bool> SaveRefreshToken(int userId, string refreshToken, DateTime expiry)
        {
            using (var conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("UpdateRefreshToken", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@UserID", userId);
                cmd.Parameters.AddWithValue("@RefreshToken", refreshToken);
                cmd.Parameters.AddWithValue("@RefreshTokenExpiry", expiry);

                conn.Open();
                int result = await cmd.ExecuteNonQueryAsync();
                return result > 0;
            }
        }

        public async Task<UserModel?> GetUserByRefreshToken(string refreshToken)
        {
            using (var conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("GetUserByRefreshToken", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@RefreshToken", refreshToken);

                conn.Open();
                SqlDataReader reader = await cmd.ExecuteReaderAsync();

                if (reader.Read())
                {
                    return new UserModel
                    {
                        UserID = Convert.ToInt32(reader["UserID"]),
                        UserName = reader["UserName"].ToString(),
                        Email = reader["Email"].ToString(),
                        Role = reader["Role"].ToString(),
                        RefreshToken = reader["RefreshToken"].ToString(),
                        RefreshTokenExpiry = Convert.ToDateTime(reader["RefreshTokenExpiry"])
                    };
                }
            }
            return null;
        }

    }
}
