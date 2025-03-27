using TelephoneDirApi.Model;

namespace TelephoneDirApi.DAL.PersonsRepository
{
    public interface IPersonRepository
    {
        List<Person> GetAllPersons(int pageNumber, int pageSize);
        Person GetPersonById(int id);
        string InsertPerson(PersonINUP person);
        string UpdatePerson(PersonINUP person);
        string DeletePerson(int personId);
        List<Person> SearchPersons(string searchTerm, string gender, int offsetNo, int nextFetchNo, out int totalRecords);
    }
}
