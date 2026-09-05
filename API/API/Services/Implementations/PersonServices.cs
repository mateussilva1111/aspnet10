using API.Data.Converter.Implementation;
using API.Data.Dto;
using API.Models;
using API.Repositories;
using Mapster;

namespace API.Services.Implementations
{
    public class PersonServices : IPersonServices
    {
        private IPersonRepository _repository;
        private PersonConverter _converter;

        public PersonServices(IPersonRepository repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public async Task<IEnumerable<PersonDTO>> GetAllAsync()
        {
            var peoples = await _repository.GetAllAsync();
            return _converter.ParseList(peoples.ToList());
        }

        public async Task<PersonDTO?> GetByIdAsync(long id)
        {
            var person = await _repository.GetByIdAsync(id);
            return _converter.Parse(person);    
        }

        public async Task<PersonDTO> CreateAsync(PersonDTO person)
        {
            var personModel = _converter.Parse(person);
            var createdPerson = await _repository.CreateAsync(personModel);
            return _converter.Parse(createdPerson);
        }

        public async Task<PersonDTO?> UpdateAsync(PersonDTO person)
        {
            var existingPerson =  await _repository.GetByIdAsync(person.Id);
            if (existingPerson == null)
                return null;

            var personModel = _converter.Parse(person);
            await _repository.UpdateAsync(personModel);

            return _converter.Parse(personModel);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var existingPerson = await  _repository.GetByIdAsync(id);
            if (existingPerson == null)
                return false;

            return await _repository.DeleteAsync(id);
        }

        public async Task<PersonDTO?> DisableAsync(long id)
        {
            
            var entity = await _repository.Disable(id);
            return entity?.Adapt<PersonDTO>();
        }
    }
}