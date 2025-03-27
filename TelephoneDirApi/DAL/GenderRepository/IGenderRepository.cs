using TelephoneDirApi.Model;

namespace TelephoneDirApi.DAL.GenderRepository
{
    public interface IGenderRepository
    {
        List<Gender> GetAllGenders();
        Gender GetGenderById(int id);
        void InsertGender(string genderName);
        void UpdateGender(int id, string genderName);
        string DeleteGender(int id);
    }
}
