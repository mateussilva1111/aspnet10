using API.Models;
using API.Models.Context;

namespace API.Repositories.Implementations
{
    public class PersonRepository(MSSQLContext context) 
        : GenericRepository<Person>(context), IPersonRepository
    {


        // Implement the IPersonRepository member
        public Task<Person> Disable(long id)
        {
            var person = GetByIdAsync(id).Result;
            if (person == null) return null;
            person.Enabled = false;
            _context.SaveChanges();
           return Task.FromResult(person);
        }
    }
}
