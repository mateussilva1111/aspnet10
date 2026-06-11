using API.Models;

namespace API.Services
{
    public interface IBooksServices
    {
        Task<IEnumerable<Books>> GetAllAsync();
        Task<Books> GetByIdAsync(long id);
        Task<Books> CreateAsync(Books book);
        Task<Books> UpdateAsync(Books book);
        Task<bool> DeleteAsync(long id);
    }
}
