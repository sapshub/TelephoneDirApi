using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TelephoneDirApi.DAL.CountryRepository;
using TelephoneDirApi.Model;

namespace TelephoneDirApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;

        public CountryController(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Country>> GetAll()
        {
            return Ok(_countryRepository.GetAllCountry());
        }

        // ✅ GET: api/country/{id}
        [HttpGet("{id}")]
        public ActionResult<Country> Get(int id)
        {
            var country = _countryRepository.GetCountryById(id);
            if (country == null)
                return NotFound();
            return Ok(country);
        }

        // ✅ POST: api/country
        [HttpPost]
        public ActionResult Post([FromBody] Country country)
        {
            _countryRepository.InsertCountry(country.CountryID, country.CountryName);
            return Ok("Country Added Successfully");
        }

        // ✅ PUT: api/country/{id}
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Country country)
        {
            _countryRepository.UpdateCountry(id, country.CountryName);
            return Ok("Country Updated Successfully");
        }

        // ✅ DELETE: api/country/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var message = _countryRepository.DeleteCountry(id);
            return Ok(message);
        }
    }
}
