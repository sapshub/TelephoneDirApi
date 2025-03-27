using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TelephoneDirApi.DAL.CityRepostirory;
using TelephoneDirApi.DAL.CountryRepository;
using TelephoneDirApi.DAL.StatesRepository;
using TelephoneDirApi.Filter;
using TelephoneDirApi.Model;

namespace TelephoneDirApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly IStateRepository _stateRepository;
        private readonly ICountryRepository _countryRepository;
        private readonly ICityRepository _cityRepository;
        public CityController(IStateRepository stateRepository, ICountryRepository countryRepository, ICityRepository cityRepository)
        {
            _stateRepository = stateRepository;
            _countryRepository = countryRepository;
            _cityRepository = cityRepository;
        }
        [ServiceFilter(typeof(ExecutionTimeFilter))]
        [HttpGet]
        public IActionResult GetAllCities()
        {
            var cities = _cityRepository.GetAllCities();
            return Ok(cities);
        }

        // GET: api/City/{id}
        [HttpGet("{id}")]
        public IActionResult GetCityById(int id)
        {
            var city = _cityRepository.GetCityById(id);
            if (city == null)
            {
                return NotFound("City not found.");
            }
            return Ok(city);
        }

        // POST: api/City
        [HttpPost]
        public IActionResult CreateCity([FromBody] City city)
        {
            if (city == null)
            {
                return BadRequest("Invalid city data.");
            }

            // Check if StatesID exists
            var stateExists = _stateRepository.GetStateById(city.StatesID);
            if (stateExists == null)
            {
                return BadRequest("Error: The provided StatesID does not exist.");
            }

            // Check if CountryID exists
            var countryExists = _countryRepository.GetCountryById(city.CountryID);
            if (countryExists == null)
            {
                return BadRequest("Error: The provided CountryID does not exist.");
            }

            _cityRepository.InsertCity(city.CityID, city.CityName, city.StatesID, city.CountryID);
            return Ok("City added successfully.");
        }

        // PUT: api/City/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateCity(int id, [FromBody] City city)
        {
            var existingCity = _cityRepository.GetCityById(id);
            if (existingCity == null)
            {
                return NotFound("Error: City not found.");
            }

            // Check if StatesID exists
            var stateExists = _stateRepository.GetStateById(city.StatesID);
            if (stateExists == null)
            {
                return BadRequest("Error: The provided StatesID does not exist.");
            }

            // Check if CountryID exists
            var countryExists = _countryRepository.GetCountryById(city.CountryID);
            if (countryExists == null)
            {
                return BadRequest("Error: The provided CountryID does not exist.");
            }

            _cityRepository.UpdateCity(id, city.CityName, city.StatesID, city.CountryID);
            return Ok("City updated successfully.");
        }

        // DELETE: api/City/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteCity(int id)
        {
            var existingCity = _cityRepository.GetCityById(id);
            if (existingCity == null)
            {
                return NotFound("Error: City not found.");
            }

            var result = _cityRepository.DeleteCity(id);
            return Ok(result);
        }
    }
}
