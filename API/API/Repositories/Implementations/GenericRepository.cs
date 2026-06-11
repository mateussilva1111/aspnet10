using API.Models.Base;
using API.Models.Context;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories.Implementations
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        private MSSQLContext _context;
        private DbSet<T> _dataSet;

        public GenericRepository(MSSQLContext context)
        {
            _context = context;
            _dataSet = context.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dataSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(long id)
        {
            return await _dataSet.FindAsync(id);
        }
        
        public async Task<T> CreateAsync(T item)
        {
            _dataSet.Add(item);
            _context.SaveChangesAsync();

            return item;
        }

        public async Task<T> UpdateAsync(T item)
        {
            var existingItem = await _dataSet.FindAsync(item.Id);

            if (existingItem == null)
                return null;

            _dataSet.Entry(existingItem).CurrentValues.SetValues(item);

            _context.SaveChangesAsync();

            return existingItem;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var existingitem = await _dataSet.FindAsync(id);
            if (existingitem == null)
                return false;

            _dataSet.Remove(existingitem);

            _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Exists(long id)
        {
            return await _dataSet.AnyAsync(x => x.Id == id);
        }

    }
}
