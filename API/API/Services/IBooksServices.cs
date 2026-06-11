using API.Models;

namespace API.Services
{
    public interface IBooksServices
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(long id);
        Task<Book> CreateAsync(Book book);
        Task<Book> UpdateAsync(Book book);
        Task<bool> DeleteAsync(long id);
    }
}
