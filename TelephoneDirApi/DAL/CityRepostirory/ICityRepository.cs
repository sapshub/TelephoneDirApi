using TelephoneDirApi.ViewModel;

namespace TelephoneDirApi.DAL.CityRepostirory
{
    public interface ICityRepository
    {
        List<CityViewModel> GetAllCities();
        CityViewModel GetCityById(int id);
        void InsertCity(int cityID, string cityName, int statesID, int countryID);
        void UpdateCity(int cityID, string cityName, int statesID, int countryID);
        string DeleteCity(int cityID);
    }
}
