using TelephoneDirApi.DTOs;

namespace TelephoneDirApi.DAL.CityRepostirory
{
    public interface ICityRepository
    {
        List<CityDTO> GetAllCities();
        CityDTO GetCityById(int id);
        void InsertCity(int cityID, string cityName, int statesID, int countryID);
        void UpdateCity(int cityID, string cityName, int statesID, int countryID);
        string DeleteCity(int cityID);
    }
}
