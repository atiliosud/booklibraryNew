using BookLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BookLibrary.Repositories
{
    public class BookRepository<T> : IBookRepository<T> where T : class
    {
        private readonly BookLibraryContext _context;

        public BookRepository(BookLibraryContext context)
        {
            _context = context;
        }

        public async Task<T?> GetByIdAsync(int id) 
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity; 
        }

       
        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
                return true;  
            }
            return false;  
        }
    }
}
