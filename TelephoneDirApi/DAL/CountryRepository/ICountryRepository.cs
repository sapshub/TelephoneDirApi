using TelephoneDirApi.Model;

namespace TelephoneDirApi.DAL.CountryRepository
{
    public interface ICountryRepository
    {
        List<Country> GetAllCountry();
        Country GetCountryById(int id);
        void InsertCountry(int countryID, string countryName);
        void UpdateCountry(int countryID, string countryName);
        string DeleteCountry(int countryID);
    }
}
