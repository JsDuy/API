using APITeaching_172100251.Data;
using Microsoft.EntityFrameworkCore;

namespace APITeaching_172100251.Repositories.SecondApproach
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetsAsync();
        Task<T> CreateAsync(T dto);
        int Delete(T dto);
        Task<T> UpdateAsync(T dto);
    }
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApiteachingContext _context;
        protected readonly DbSet<T> _dbSet;
        public Repository(ApiteachingContext dbContext)
        {
            _context = dbContext;
            _dbSet = _context.Set<T>();
        }
        public async Task<T> GetAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task<IEnumerable<T>> GetsAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }
        public int Delete(T dto)
        {
            try
            {
                _context.Entry(dto).State = EntityState.Deleted;
                var result = _context.SaveChanges();
                return result;
            }
            catch
            {
                return 0;
            }
        }
    }
}
