using API.Models;
using API.Repositories;

namespace API.Services.Implementations
{
    public class BooksServices : IBooksServices
    {
        private IRepository<Book> _repository;

        public BooksServices(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Book?> GetByIdAsync(long id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Book> CreateAsync(Book books)
        {
            return await _repository.CreateAsync(books);
        }

        public async Task<Book?> UpdateAsync(Book books)
        {
            var existingBook = await _repository.GetByIdAsync(books.Id);
            if (existingBook == null)
                return null;

            await _repository.UpdateAsync(books);

            return books;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var existingBook = await _repository.GetByIdAsync(id);
            if (existingBook == null)
                return false;

            return await _repository.DeleteAsync(id);
        }
    }
}
