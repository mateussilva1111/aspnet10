using API.Models;
using API.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implementations
{
    public class BooksRepository : IBooksRepository
    {
        private MSSQLContext _context;

        public BooksRepository(MSSQLContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Books>> GetAllAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Books?> GetByIdAsync(long id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<Books> CreateAsync(Books book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return book;
        }

        public async Task<Books?> UpdateAsync(Books book)
        {
            var existingBook = await _context.Books.FindAsync(book.Id);

            if (existingBook == null)
                return null;

            _context.Entry(existingBook).CurrentValues.SetValues(book);

            await _context.SaveChangesAsync();

            return existingBook;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var existingBook = await _context.Books.FindAsync(id);
            if (existingBook == null)
                return false;

            _context.Books.Remove(existingBook);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
