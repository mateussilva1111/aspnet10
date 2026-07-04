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
            return (await _repository.GetAllAsync())
                .Adapt<List<BookDto>>();
        }

        public async Task<BookDto> GetByIdAsync(long id)        
        {
            var entity = await _repository.GetByIdAsync(id); 
            return entity.Adapt<BookDto>();
        }

        public async Task<BookDto> CreateAsync(BookDto book)
        {
            var entity = book.Adapt<Book>();
            var created = await _repository.CreateAsync(entity);
            return created.Adapt<BookDto>();
        }

        public async Task<BookDto?> UpdateAsync(BookDto book)
        {
            var existing = await _repository.GetByIdAsync(book.Id);
            if (existing == null)
                return null;

            var entity = book.Adapt<Book>();
            var updated = await _repository.UpdateAsync(entity);

            return updated.Adapt<BookDto>();
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null)
                return false;

            return await _repository.DeleteAsync(id);
        }
    }
}
