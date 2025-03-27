using Microsoft.Data.SqlClient;
using System.Data;
using TelephoneDirApi.DAL.BaseRepositoryy;
using TelephoneDirApi.Model;
using TelephoneDirApi.ViewModel;

namespace TelephoneDirApi.DAL.StatesRepository
{
    public class StateRepository : BaseRepository, IStateRepository
    {
        public StateRepository(IConfiguration configuration):base(configuration) { }
        
        public List <StatesViewModel> GetAllStates()
        {
            var states = new List<StatesViewModel>();
            using (var conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("GetStates", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        states.Add(new StatesViewModel
                        {
                            StateID = Convert.ToInt32(reader["StatesID"]),
                            StateName = reader["StateName"].ToString(),
                            CountryName = reader["CountryName"].ToString()
                        });
                    }
                }
            }
            return states;
        }
        public StatesViewModel GetStateById(int id)
        {
            StatesViewModel state = null;
            using (var conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("GetStates", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@StatesID", id);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        state = new StatesViewModel
                        {
                            StateID = Convert.ToInt32(reader["StatesID"]),
                            StateName = reader["StateName"].ToString(),
                            CountryName = reader["CountryName"].ToString()
                        };
                    }
                }
            }
            return state;
        }

        public string InsertState(int StatesID, string stateName, int countryID)
        {
            using (var conn = GetConnection())
            {
                conn.Open();

                // Check if the CountryID exists
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(1) FROM Country WHERE CountryID = @CountryID", conn);
                checkCmd.Parameters.AddWithValue("@CountryID", countryID);

                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count == 0)
                {
                    return "Error: The provided CountryID does not exist.";
                }

                // If CountryID exists, proceed with the insert
                SqlCommand cmd = new SqlCommand("insertstates", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@StatesID", StatesID);
                cmd.Parameters.AddWithValue("@StateName", stateName);
                cmd.Parameters.AddWithValue("@CountryID", countryID);

                cmd.ExecuteNonQuery();
                return "State inserted successfully.";
            }
        }

        public string UpdateState(int StatesID, string stateName, int countryID)
        {
            using (var conn = GetConnection())
            {
                conn.Open();

                // Check if the CountryID exists
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(1) FROM Country WHERE CountryID = @CountryID", conn);
                checkCmd.Parameters.AddWithValue("@CountryID", countryID);

                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count == 0)
                {
                    return "Error: The provided CountryID does not exist.";
                }

                // If CountryID exists, proceed with the update
                SqlCommand cmd = new SqlCommand("updatestates", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@StatesID", StatesID);
                cmd.Parameters.AddWithValue("@StateName", stateName);
                cmd.Parameters.AddWithValue("@CountryID", countryID);

                cmd.ExecuteNonQuery();
                return "State updated successfully.";
            }
        }

        public string DeleteState(int StatesID)
        {
            using (var conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("deletestates", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@StatesID", StatesID);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0 ? "Deleted Successfully" : "Delete Failed";
            }
        }
    }
}
