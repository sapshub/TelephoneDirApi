using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TelephoneDirApi.DAL.CountryRepository;
using TelephoneDirApi.DAL.StatesRepository;
using TelephoneDirApi.Model;

namespace TelephoneDirApi.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class StatesController : ControllerBase
    {
       private readonly IStateRepository _stateRepository;
        private readonly ICountryRepository _countryRepository;
        public StatesController(IStateRepository stateRepository, ICountryRepository countryRepository)
        {
            _stateRepository = stateRepository;
            _countryRepository = countryRepository;
        }

        [HttpGet]
        public ActionResult<List<States>> GetStates()
        {
            return Ok(_stateRepository.GetAllStates());
        }

        // GET: api/States/{id}
        [HttpGet("{id}")]
        public ActionResult<States> GetStateById(int id)
        {
            var state = _stateRepository.GetStateById(id);
            if (state == null)
            {
                return NotFound("State not found.");
            }
            return Ok(state);
        }

        // POST: api/States
        [HttpPost]
        public IActionResult CreateState([FromBody] States state)
        {
            if (state == null)
            {
                return BadRequest("Invalid state data.");
            }
            var countryExists = _countryRepository.GetCountryById(state.CountryID);
            if (countryExists == null)
            {
                return BadRequest("Error: The provided CountryID does not exist.");
            }

            _stateRepository.InsertState(state.StateID, state.StateName, state.CountryID);
            return Ok("State added successfully.");
        }

        // PUT: api/States/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateState(int id, [FromBody] States state)
        {
            var existingState = _stateRepository.GetStateById(id);
            if (existingState == null)
            {
                return NotFound("State not found.");
            }
            var countryExists = _countryRepository.GetCountryById(state.CountryID);
            if (countryExists == null)
            {
                return BadRequest("Error: The provided CountryID does not exist.");
            }

            _stateRepository.UpdateState(id, state.StateName, state.CountryID);
            return Ok("State updated successfully.");
        }

        // DELETE: api/States/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteState(int id)
        {
            var existingState = _stateRepository.GetStateById(id);
            if (existingState == null)
            {
                return NotFound("State not found.");
            }
            string result = _stateRepository.DeleteState(id);
            return Ok(result);
        }
    }
}
