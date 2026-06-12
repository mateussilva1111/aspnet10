using API.Data.Dto;
using API.Models;

namespace API.Services
{
    public interface IBooksServices
    {
        Task<IEnumerable<BookDto>> GetAllAsync();
        Task<BookDto> GetByIdAsync(long id);
        Task<BookDto> CreateAsync(BookDto book);
        Task<BookDto> UpdateAsync(BookDto book);
        Task<bool> DeleteAsync(long id);
    }
}
