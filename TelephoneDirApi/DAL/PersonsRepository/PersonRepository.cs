using Microsoft.Data.SqlClient;
using System.Data;
using TelephoneDirApi.DAL.BaseRepositoryy;
using TelephoneDirApi.Model;

namespace TelephoneDirApi.DAL.PersonsRepository
{
    public class PersonRepository:BaseRepository,IPersonRepository
    {
        public PersonRepository(IConfiguration configuration) : base(configuration) { }

        public List<Person> GetAllPersons(int pageNumber, int pageSize)
        {
            var persons = new List<Person>();
            using (var conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("GetPersonsModi", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
                cmd.Parameters.AddWithValue("@PageSize", pageSize);
                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        persons.Add(new Person
                        {
                            PersonID = Convert.ToInt32(reader["PersonID"]),
                            FirstName = reader["FirstName"].ToString(),
                            MiddleName = reader["MiddleName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Emails = reader["Emails"].ToString(),
                            Phones = reader["Phones"].ToString(),
                            //GenderID = Convert.ToInt32(reader["GenderID"]),
                            //CountryID = Convert.ToInt32(reader["CountryID"]),
                            //StatesID = Convert.ToInt32(reader["StatesID"]),
                            //CityID = Convert.ToInt32(reader["CityID"]),
                            GenderName = reader["Gender_name"].ToString(),   // Fetch Gender Name
                            CountryName = reader["CountryName"].ToString(), // Fetch Country Name
                            StateName = reader["StateName"].ToString(),     // Fetch State Name
                            CityName = reader["CityName"].ToString(),
                            Comments = reader["Comments"].ToString(),
                            Checkbox = Convert.ToBoolean(reader["Checkbox"])
                        });
                    }
                }
            }
            return persons;
        }

        public Person GetPersonById(int id)
        {
            Person person = null;
            using (var conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("GetPersonsModi", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@PersonID", id);

                conn.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        person = new Person
                        {
                            PersonID = Convert.ToInt32(reader["PersonID"]),
                            FirstName = reader["FirstName"].ToString(),
                            MiddleName = reader["MiddleName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Emails = reader["Emails"].ToString(),
                            Phones = reader["Phones"].ToString(),
                            //GenderID = Convert.ToInt32(reader["GenderID"]),
                            //CountryID = Convert.ToInt32(reader["CountryID"]),
                            //StatesID = Convert.ToInt32(reader["StatesID"]),
                            //CityID = Convert.ToInt32(reader["CityID"]),
                            GenderName = reader["Gender_name"].ToString(),   // Fetch Gender Name
                            CountryName = reader["CountryName"].ToString(), // Fetch Country Name
                            StateName = reader["StateName"].ToString(),     // Fetch State Name
                            CityName = reader["CityName"].ToString(),
                            Comments = reader["Comments"].ToString(),
                            Checkbox = Convert.ToBoolean(reader["Checkbox"])
                        };
                    }
                }
            }
            return person;
        }

        public string InsertPerson(PersonINUP person)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("InsertPersonModi", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@FirstName", person.FirstName);
                cmd.Parameters.AddWithValue("@MiddleName", person.MiddleName);
                cmd.Parameters.AddWithValue("@LastName", person.LastName);
                cmd.Parameters.AddWithValue("@Emails", person.Emails);
                cmd.Parameters.AddWithValue("@Phones", person.Phones);
                cmd.Parameters.AddWithValue("@GenderID", person.GenderID);
                cmd.Parameters.AddWithValue("@CountryID", person.CountryID);
                cmd.Parameters.AddWithValue("@StatesID", person.StatesID);
                cmd.Parameters.AddWithValue("@CityID", person.CityID);
                cmd.Parameters.AddWithValue("@Comments", person.Comments);
                cmd.Parameters.AddWithValue("@Checkbox", person.Checkbox);

                cmd.ExecuteNonQuery();
                return "Person inserted successfully.";
            }
        }

        public string UpdatePerson(PersonINUP person)
        {
            using (var conn = GetConnection())
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UpdatePersonModi", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@PersonID", person.PersonID);
                cmd.Parameters.AddWithValue("@FirstName", person.FirstName);
                cmd.Parameters.AddWithValue("@MiddleName", person.MiddleName);
                cmd.Parameters.AddWithValue("@LastName", person.LastName);
                cmd.Parameters.AddWithValue("@Emails", person.Emails);
                cmd.Parameters.AddWithValue("@Phones", person.Phones);
                cmd.Parameters.AddWithValue("@GenderID", person.GenderID);
                cmd.Parameters.AddWithValue("@CountryID", person.CountryID);
                cmd.Parameters.AddWithValue("@StatesID", person.StatesID);
                cmd.Parameters.AddWithValue("@CityID", person.CityID);
                cmd.Parameters.AddWithValue("@Comments", person.Comments);
                cmd.Parameters.AddWithValue("@Checkbox", person.Checkbox);

                cmd.ExecuteNonQuery();
                return "Person updated successfully.";
            }
        }

        public string DeletePerson(int personId)
        {
            using (var conn = GetConnection())
            {
                SqlCommand cmd = new SqlCommand("DeletePersonModi", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@PersonID", personId);

                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                return rowsAffected > 0 ? "Deleted Successfully" : "Delete Failed";
            }
        }

        public List<Person> SearchPersons(string searchTerm,string gender,int offsetNo, int nextFetchNo, out int totalRecords)
        {
            List<Person> persons = new List<Person>();
            totalRecords = 0;

            using (var conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand("SearchPersonData", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add search term parameter
                    cmd.Parameters.AddWithValue("@SearchTerm", (object)searchTerm ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Gender", (object)gender ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@offsetno", offsetNo);
                    cmd.Parameters.AddWithValue("@nextfetchno", nextFetchNo);

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        // Read the total record count
                        if (reader.Read())
                        {
                            totalRecords = Convert.ToInt32(reader["TotalRecords"]);
                        }

                        // Move to next result set
                        //if (reader.NextResult())
                        if (reader.NextResult())
                        {
                            while (reader.Read())
                            {
                                persons.Add(new Person
                                {
                                    PersonID = Convert.ToInt32(reader["PersonID"]),
                                    FirstName = reader["FirstName"].ToString(),
                                    MiddleName = reader["MiddleName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    GenderName = reader["Gender"].ToString(),
                                    CountryName = reader["CountryName"].ToString(),
                                    StateName = reader["StateName"].ToString(),
                                    CityName = reader["CityName"].ToString(),
                                    Emails = reader["Email"].ToString(),
                                    Phones= reader["Phone"].ToString(),
                                    Comments = reader["Comments"].ToString(),
                                    Checkbox = Convert.ToBoolean(reader["Checkbox"])
                                });
                            }
                        }
                    }
                }
            }

            return persons;
        }


    }
}
