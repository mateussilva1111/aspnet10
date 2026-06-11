using API.Models;

namespace API.Repositories
{
    public interface IBooksRepository
    {
        Task<IEnumerable<Books>> GetAllAsync();
        Task<Books> GetByIdAsync(long id);
        Task<Books> CreateAsync(Books book);
        Task<Books> UpdateAsync(Books book);
        Task<bool> DeleteAsync(long id);
    }
}
