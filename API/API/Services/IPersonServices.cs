using API.Models;

namespace API.Services
{
    public interface IPersonServices
    {

        Task<IEnumerable<Person>> GetAllAsync();
        Task<Person> GetByIdAsync(long id);
        Task<Person> CreateAsync(Person person);
        Task<Person> UpdateAsync(Person person);
        Task<bool> DeleteAsync(long id);
    }
}
