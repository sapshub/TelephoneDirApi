using System.Data;
using Microsoft.Data.SqlClient;
using TelephoneDirApi.DAL.BaseRepositoryy;
using TelephoneDirApi.Model;

namespace TelephoneDirApi.DAL.GenderRepository
{
    public class GenderRepository : BaseRepository, IGenderRepository
    {
        private readonly string _connectionString;
        public GenderRepository(IConfiguration configuration) : base(configuration) { }

        public List<Gender> GetAllGenders()
        {
            var genders = new List<Gender>();
            using (var conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("GetGenders", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        genders.Add(new Gender
                        {
                            GenderID = Convert.ToInt32(reader["GenderID"]),
                            Gender_name = reader["Gender_name"].ToString()
                        });
                    }
                }
            }
            return genders;
        }

        public Gender GetGenderById(int id)
        {
            Gender gender = null;
            using (var conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("GetGenders", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@GenderId", id);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        gender = new Gender
                        {
                            GenderID = Convert.ToInt32(reader["GenderID"]),
                            Gender_name = reader["Gender_name"].ToString()
                        };
                    }
                }
            }
            return gender;
        }
        public void InsertGender(string genderName)
        {
            using (var conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("insertgender", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@newGender", genderName);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        public void UpdateGender(int id, string genderName)
        {
            using (var conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("updategender", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@GenderID", id);
                cmd.Parameters.AddWithValue("@newGender", genderName);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public string DeleteGender(int id)
        {
            using (var conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("deletegender", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@GenderID", id);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0 ? "Deleted Successfully" : "Delete Failed";
            }
        }
    }
}
