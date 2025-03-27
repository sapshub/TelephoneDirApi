using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TelephoneDirApi.DAL.GenderRepository;
using TelephoneDirApi.Model;

namespace TelephoneDirApi.Controllers
{
    [Authorize(Roles ="Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class GenderController : ControllerBase
    {
        private readonly IGenderRepository _genderRepository;
        public GenderController(IGenderRepository genderRepository)
        {
            _genderRepository = genderRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Gender>> GetAll()
        {
            return Ok(_genderRepository.GetAllGenders());
        }
        [HttpGet("{id}")]
        public ActionResult<Gender> Get(int id)
        {
            var gender = _genderRepository.GetGenderById(id);
            if (gender == null)
                return NotFound();
            return Ok(gender);
        }

        // POST: api/gender
        [HttpPost]
        public ActionResult Post([FromBody] Gender gender)
        {
            _genderRepository.InsertGender(gender.Gender_name);
            return Ok("Gender Added Successfullyy");
        }

        // PUT: api/gender/{id}
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Gender gender)
        {
            _genderRepository.UpdateGender(id, gender.Gender_name);
            return Ok("Gender Updated Successfully");
        }

        // DELETE: api/gender/{id}
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var message = _genderRepository.DeleteGender(id);
            return Ok(message);
        }
    }
}
