using API.Models;
using API.Repositories;

namespace API.Services.Implementations
{
    public class BooksServices : IBooksServices
    {
        private IBooksRepository _booksRepository;

        public BooksServices(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        public async Task<IEnumerable<Books>> GetAllAsync()
        {
            return await _booksRepository.GetAllAsync();
        }

        public async Task<Books?> GetByIdAsync(long id)
        {
            return await _booksRepository.GetByIdAsync(id);
        }

        public async Task<Books> CreateAsync(Books books)
        {
            return await _booksRepository.CreateAsync(books);

        }

        public async Task<Books?> UpdateAsync(Books books)
        {
            var existingBook = await _booksRepository.GetByIdAsync(books.Id);
            if (existingBook == null)
                return null;

            _booksRepository.UpdateAsync(books);

            return books;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var existingBook = await _booksRepository.GetByIdAsync(id);
            if (existingBook == null)
                return false;

            return await _booksRepository.DeleteAsync(id);
        }
    }
}
