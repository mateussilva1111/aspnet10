using API.Models;
using API.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace API.Services.Implementations
{
    public class PersonServices : IPersonServices
    {
        private readonly MSSQLContext _context;

        public PersonServices(MSSQLContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _context.Persons.ToListAsync();
        }

        public async Task<Person?> GetByIdAsync(long id)
        {
            return await _context.Persons.FindAsync(id);
        }

        public async Task<Person> CreateAsync(Person person)
        {
            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            return person;
        }

        public async Task<Person?> UpdateAsync(Person person)
        {
            var existingPerson = _context.Persons.FindAsync(person.Id);
            if (existingPerson == null)
                return null;

            _context.Entry(existingPerson).CurrentValues.SetValues(person);

            await _context.SaveChangesAsync();

            return person;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var existingPerson = await _context.Persons.FindAsync(id);
            if (existingPerson == null)
                return false;

            _context.Persons.Remove(existingPerson);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}