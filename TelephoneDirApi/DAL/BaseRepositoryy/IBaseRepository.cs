using Microsoft.Data.SqlClient;

namespace TelephoneDirApi.DAL.BaseRepositoryy
{
    public interface IBaseRepository
    {
        SqlConnection GetConnection();
    }
}
