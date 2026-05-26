using API.Models;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private IPersonServices _personServices;
        public PersonController(IPersonServices personServices)
        {
            _personServices = personServices;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            var people = await _personServices.GetAllAsync();
            return Ok(people);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var person = await _personServices.GetByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Person person)
        {
            return Ok( await _personServices.CreateAsync(person));
        }

        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] Person person)
        {
            return Ok(await _personServices.UpdateAsync(person));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _personServices.DeleteAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok();
        }

    }
}
