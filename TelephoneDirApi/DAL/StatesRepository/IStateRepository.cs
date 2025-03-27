using TelephoneDirApi.ViewModel;

namespace TelephoneDirApi.DAL.StatesRepository
{
    public interface IStateRepository
    {
        List<StatesViewModel> GetAllStates();
        StatesViewModel GetStateById(int id);
        string InsertState(int StatesID, string stateName, int countryID);
        string UpdateState(int StatesID, string stateName, int countryID);
        string DeleteState(int StatesID);
    }
}
