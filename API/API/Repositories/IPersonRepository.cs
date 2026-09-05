using API.Models;

namespace API.Repositories
{
    public interface IPersonRepository : IRepository<Person>
    {
        Task<Person> Disable(long id);
    }
}
