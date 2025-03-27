using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TelephoneDirApi.DAL.CityRepostirory;
using TelephoneDirApi.DAL.CountryRepository;
using TelephoneDirApi.DAL.GenderRepository;
using TelephoneDirApi.DAL.PersonsRepository;
using TelephoneDirApi.DAL.StatesRepository;
using TelephoneDirApi.Model;

namespace TelephoneDirApi.Controllers
{

    [Authorize(Roles = "User")]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;
        private readonly IStateRepository _stateRepository;
        private readonly ICityRepository _cityRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly IGenderRepository _genderRepository;
        public PersonController(IPersonRepository personRepository,IGenderRepository genderRepository, ICityRepository cityRepository, IStateRepository stateRepository, ICountryRepository countryRepository)
        {
            _personRepository = personRepository; 
            _stateRepository = stateRepository;
            _cityRepository = cityRepository;
            _countryRepository = countryRepository;
            _genderRepository = genderRepository;
        }

        [HttpGet]
        public ActionResult<List<Person>> GetAllPersons(int pageNumber = 1, int pageSize = 10)
        {
            var persons = _personRepository.GetAllPersons(pageNumber, pageSize);
            return Ok(persons);
        }

        // GET: api/person/{id}
        [HttpGet("{id}")]
        public ActionResult<Person> GetPersonById(int id)
        {
            var person = _personRepository.GetPersonById(id);
            if (person == null) return NotFound("Person not found.");
            return Ok(person);
        }

        // POST: api/person
        [HttpPost]
        public ActionResult<string> CreatePerson([FromBody] PersonINUP person)
        {
            if (person == null)
            {
                return BadRequest("Invalid person data.");
            }

            var countryExists = _countryRepository.GetCountryById(person.CountryID);
            if (countryExists == null)
            {
                return BadRequest("Error: The provided CountryID does not exist.");
            }
            var stateExists = _stateRepository.GetStateById(person.StatesID);
            if (stateExists == null)
            {
                return BadRequest("Error: The provided StateID does not exist.");
            }
            var cityExists = _cityRepository.GetCityById(person.CityID);
            if (cityExists == null)
            {
                return BadRequest("Error: The provided CityID does not exist.");
            }
            var genderExists = _genderRepository.GetGenderById(person.GenderID);
            if (countryExists == null)
            {
                return BadRequest("Error: The provided GenderID does not exist.");
            }

            var result = _personRepository.InsertPerson(person);
            return Ok(result);
        }

        // PUT: api/person/{id}
        [HttpPut("{id}")]
        public ActionResult<string> UpdatePerson(int id, [FromBody] PersonINUP person)
        {
            
            var countryExists = _countryRepository.GetCountryById(person.CountryID);
            if (countryExists == null)
            {
                return BadRequest("Error: The provided CountryID does not exist.");
            }
            var stateExists = _stateRepository.GetStateById(person.StatesID);
            if (stateExists == null)
            {
                return BadRequest("Error: The provided StateID does not exist.");
            }
            var cityExists = _cityRepository.GetCityById(person.CityID);
            if (cityExists == null)
            {
                return BadRequest("Error: The provided CityID does not exist.");
            }
            var genderExists = _genderRepository.GetGenderById(person.GenderID);
            if (countryExists == null)
            {
                return BadRequest("Error: The provided GenderID does not exist.");
            }

            person.PersonID = id;
            var result = _personRepository.UpdatePerson(person);
            return Ok(result);
        }

        // DELETE: api/person/{id}
        [HttpDelete("{id}")]
        public ActionResult<string> DeletePerson(int id)
        {
            var result = _personRepository.DeletePerson(id);
            return Ok(result);
        }

        [HttpGet("search")]
        public IActionResult SearchPersons([FromQuery] string searchTerm=null, [FromQuery] string gender=null, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
        {
            //try
            //{
            //    int offsetNo = (pageNumber - 1) * pageSize;
            //    int totalRecords;

            //    var persons = _personRepository.SearchPersons(searchTerm, gender, offsetNo, pageSize, out totalRecords);

            //    var response = new
            //    {
            //        Data = persons,
            //        TotalRecords = totalRecords,
            //        CurrentPage = pageNumber,
            //        PageSize = pageSize,
            //        TotalPages = (int)Math.Ceiling((double)totalRecords / pageSize)
            //    };

            //    return Ok(response);
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, new { Message = "An error occurred while searching for persons.", Error = ex.Message });
            //}

            try
            {
                searchTerm = string.IsNullOrWhiteSpace(searchTerm) ? null : searchTerm;
                gender = string.IsNullOrWhiteSpace(gender) ? null : gender;
                int totalRecords;
                var persons = _personRepository.SearchPersons(searchTerm, gender, pageNumber, pageSize, out totalRecords);

                return Ok(new
                {
                    TotalRecords = totalRecords,
                    Persons = persons
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while fetching persons", Error = ex.Message });
            }
        }


    }
}
