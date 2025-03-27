using TelephoneDirApi.DTOs;


namespace TelephoneDirApi.DAL.StatesRepository
{
    public interface IStateRepository
    {
        List<StatesDTO> GetAllStates();
        StatesDTO GetStateById(int id);
        string InsertState(int StatesID, string stateName, int countryID);
        string UpdateState(int StatesID, string stateName, int countryID);
        string DeleteState(int StatesID);
    }
}
