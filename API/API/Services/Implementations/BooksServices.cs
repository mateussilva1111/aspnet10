using API.Data.Dto;
using API.Models;
using API.Repositories;
using Mapster;

namespace API.Services.Implementations
{
    public class BooksServices : IBooksServices
    {
        private IRepository<Book> _repository;

        public BooksServices(IRepository<Book> repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<BookDto>> GetAllAsync()
        {
            return  _repository.GetAllAsync().Adapt<IEnumerable<BookDto>>();
        }

        public async Task<BookDto?> GetByIdAsync(long id)
        {
            return  _repository.GetByIdAsync(id).Adapt<BookDto?>();
        }

        public async Task<BookDto> CreateAsync(BookDto book)
        {
            var entity = book.Adapt<Book>();
            return _repository.CreateAsync(entity).Adapt<BookDto>();
        }

        public async Task<BookDto?> UpdateAsync(BookDto book)
        {
            var existingBook =  _repository.GetByIdAsync(book.Id).Adapt<BookDto?>();
            if (existingBook == null)
                return null;
            var entity = book.Adapt<Book>();
            await _repository.UpdateAsync(entity);

            return entity.Adapt<BookDto>();
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var existingBook = _repository.GetByIdAsync(id).Adapt<BookDto?>();
            if (existingBook == null)
                return false;

            return await _repository.DeleteAsync(id);
        }
    }
}
