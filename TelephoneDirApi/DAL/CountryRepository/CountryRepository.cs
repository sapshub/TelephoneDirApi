using Microsoft.Data.SqlClient;
using System.Data;
using TelephoneDirApi.Model;
using TelephoneDirApi.DAL.BaseRepositoryy;

namespace TelephoneDirApi.DAL.CountryRepository
{
    public class CountryRepository : BaseRepository, ICountryRepository
    {
        public CountryRepository(IConfiguration configuration) : base(configuration) { }


        public List<Country> GetAllCountry()
        {
            var countries = new List<Country>();
            using (var conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("GetCountries", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        countries.Add(new Country
                        {
                            CountryID = Convert.ToInt32(reader["CountryID"]),
                            CountryName = reader["CountryName"].ToString()
                        });
                    }
                }
            }
            return countries;
        }

        public Country GetCountryById(int id)
        {
            Country country = null;
            using (var conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("GetCountries", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@CountryID", id);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        country = new Country
                        {
                            CountryID = Convert.ToInt32(reader["CountryID"]),
                            CountryName = reader["CountryName"].ToString()
                        };
                    }
                }
            }
            return country;
        }


      
        public void InsertCountry(int countryID, string countryName)
        {
            using (var conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("insertcountry", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@CountryID", countryID);
                cmd.Parameters.AddWithValue("@CountryName", countryName);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateCountry(int countryID, string countryName)
        {
            using (var conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("updatecountry", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@CountryID", countryID);
                cmd.Parameters.AddWithValue("@CountryName", countryName);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public string DeleteCountry(int countryID)
        {
            using (var conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("deletecountry", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@CountryID", countryID);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0 ? "Deleted Successfully" : "Delete Failed";
            }
        }
    }
}
