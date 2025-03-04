using System.Text.Json.Serialization;

namespace TelephoneDirApi.Model
{
    public class Person
    {
        public int PersonID {  get; set; }
        public string FirstName {  get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Emails { get; set; }
        public string Phones { get; set; }
        //public int GenderID { get; set; }
        //public int CountryID { get; set; }
        //public int StatesID { get; set; }
        //public int CityID { get; set; }
        public string GenderName { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }     
        public string Comments { get; set; }
        public bool Checkbox { get; set; }
    }
}
