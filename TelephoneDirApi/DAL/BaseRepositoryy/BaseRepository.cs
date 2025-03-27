using Microsoft.Data.SqlClient;

namespace TelephoneDirApi.DAL.BaseRepositoryy
{
    public abstract class BaseRepository : IBaseRepository
    {
        private readonly string _connectionString;

        protected BaseRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
