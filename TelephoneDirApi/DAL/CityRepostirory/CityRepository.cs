﻿using System.Data.SqlClient;
using System.Data;
using TelephoneDirApi.DAL.BaseRepositoryy;
using TelephoneDirApi.Model;
using TelephoneDirApi.ViewModel;

namespace TelephoneDirApi.DAL.CityRepostirory
{
    public class CityRepository : BaseRepository
    {
        public CityRepository(IConfiguration configuration) : base(configuration) { }

        public List<CityViewModel> GetAllCities()
        {
            var cities = new List<CityViewModel>();
            using (var conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("GetCities", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    cities.Add(new CityViewModel
                    {
                        CityID = Convert.ToInt32(reader["CityID"]),
                        CityName = reader["CityName"].ToString(),
                        StatesName = reader["StateName"].ToString(),
                        CountryName = reader["CountryName"].ToString()
                    });
                }
            }
            return cities;
        }

        public CityViewModel GetCityById(int id)
        {
            CityViewModel city = null;
            using (var conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("GetCities", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@CityID", id);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    city = new CityViewModel
                    {
                        CityID = Convert.ToInt32(reader["CityID"]),
                        CityName = reader["CityName"].ToString(),
                        StatesName = reader["StateName"].ToString(),
                        CountryName = reader["CountryName"].ToString()
                    };
                }
            }
            return city;
        }

        public void InsertCity(int cityID, string cityName, int statesID, int countryID)
        {
            using (var conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("insertcity", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@CityID", cityID);
                cmd.Parameters.AddWithValue("@CityName", cityName);
                cmd.Parameters.AddWithValue("@StateID", statesID);
                cmd.Parameters.AddWithValue("@CountryID", countryID);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateCity(int cityID, string cityName, int statesID, int countryID)
        {
            using (var conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("updatecity", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@CityID", cityID);
                cmd.Parameters.AddWithValue("@CityName", cityName);
                cmd.Parameters.AddWithValue("@StatesID", statesID);
                cmd.Parameters.AddWithValue("@CountryID", countryID);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public string DeleteCity(int cityID)
        {
            using (var conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("deletecity", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@CityID", cityID);
                conn.Open();

                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0 ? "Deleted Successfully" : "Delete Failed";
            }
        }
    }
}
