using API.Models;
using API.Models.Context;
using API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Implementations
{
    public class PersonServices : IPersonServices
    {
        private IPersonRepository _personRepository;

        public PersonServices(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _personRepository.GetAllAsync();
        }

        public async Task<Person?> GetByIdAsync(long id)
        {
            return await _personRepository.GetByIdAsync(id);
        }

        public async Task<Person> CreateAsync(Person person)
        {
            return await _personRepository.CreateAsync(person);
        }

        public async Task<Person?> UpdateAsync(Person person)
        {
            var existingPerson =    _personRepository.GetByIdAsync(person.Id);
            if (existingPerson == null)
                return null;

            _personRepository.UpdateAsync(person);

            return person;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var existingPerson = await _personRepository.GetByIdAsync(id);
            if (existingPerson == null)
                return false;

            return await _personRepository.DeleteAsync(id);
        }
    }
}