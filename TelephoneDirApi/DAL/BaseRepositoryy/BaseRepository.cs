using System.Data.SqlClient;

namespace TelephoneDirApi.DAL.BaseRepositoryy
{
    public abstract class BaseRepository
    {
        private readonly string _connectionString;
        protected BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
