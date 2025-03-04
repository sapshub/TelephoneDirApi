namespace TelephoneDirApi.Model
{
    public class PersonINUP
    {
        public int PersonID { get; set; }  // Include this for update
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Emails { get; set; }
        public string Phones { get; set; }
        public int GenderID { get; set; }  // IDs are needed for inserting/updating
        public int CountryID { get; set; }
        public int StatesID { get; set; }
        public int CityID { get; set; }
        public string Comments { get; set; }
        public bool Checkbox { get; set; }
    }
}
