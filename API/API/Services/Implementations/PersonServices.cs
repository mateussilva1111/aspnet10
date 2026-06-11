using API.Models;
using API.Models.Context;
using API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Implementations
{
    public class PersonServices : IPersonServices
    {
        private IRepository<Person> _repository;

        public PersonServices(IRepository<Person> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Person?> GetByIdAsync(long id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Person> CreateAsync(Person person)
        {
            return await _repository.CreateAsync(person);
        }

        public async Task<Person?> UpdateAsync(Person person)
        {
            var existingPerson =  await _repository.GetByIdAsync(person.Id);
            if (existingPerson == null)
                return null;

            _repository.UpdateAsync(person);

            return person;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var existingPerson = await  _repository.GetByIdAsync(id);
            if (existingPerson == null)
                return false;

            return await _repository.DeleteAsync(id);
        }
    }
}