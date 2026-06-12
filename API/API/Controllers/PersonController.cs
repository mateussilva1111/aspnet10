using API.Data.Dto;
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
        private ILogger<PersonController> _logger;
        public PersonController(IPersonServices personServices, ILogger<PersonController> logger)
        {
            _personServices = personServices;
            _logger = logger;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Getting all people");
            var people = await _personServices.GetAllAsync();
            if(people == null)
            {
                _logger.LogWarning("No people found");
                return NotFound();
            }
            return Ok(people);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation("Getting person with id {Id}", id);
            var person = await _personServices.GetByIdAsync(id);
            if (person == null)
            {
                _logger.LogWarning("Person with id {Id} not found", id);
                return NotFound();
            }
            return Ok(person);
        }
        
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PersonDTO person)
        {
            _logger.LogInformation("Creating a new person with name {FirstName} {LastName}", person.FirstName, person.LastName);
            var createdPerson = await _personServices.CreateAsync(person);
            if (createdPerson == null)
            {
                _logger.LogError("Failed to create person with name {FirstName} {LastName}", person.FirstName, person.LastName);
                return BadRequest();
            }
            return Ok(createdPerson);
        }

        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] PersonDTO person)
        {
            _logger.LogInformation("Updating person with id {Id}", person.Id);
            var updatedPerson = await _personServices.UpdateAsync(person);
            if (updatedPerson == null)
            {
                _logger.LogError("Failed to update person with id {Id}", person.Id);
                return BadRequest();
            }
            return Ok(updatedPerson);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting person with id {Id}", id);
            var result = await _personServices.DeleteAsync(id);
            if (!result)
            {
                _logger.LogWarning("Person with id {Id} not found", id);
                return NotFound();
            }
            return Ok();
        }

    }
}
