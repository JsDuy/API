using APITeaching_172100251.Data;
using APITeaching_172100251.Models;
using Microsoft.EntityFrameworkCore;

namespace APITeaching_172100251.Repositories.Simple
{
    public interface ICategoryService
    {
        Task<List<Category>> GetsAsync();
        Task<Category> GetAsync(int id, bool useDTO = true);
        Task<Category> CreateAsync(Category dto);
        Task<Category> UpdateAsync(Category dto);
        int Delete(int id);
        bool CheckExists(int id);
    }

    public class CategoryService : ICategoryService
    {
        private readonly ApiteachingContext _context;
        public CategoryService(ApiteachingContext context)
        {
            _context = context;
        }
        public async Task<Category> CreateAsync(Category dto)
        {
            _context.Categories.Add(dto);
            if (await _context.SaveChangesAsync() > 0)
                return dto;

            return null!;
        }

        public int Delete(int id)
        {
            var entity = _context.Categories.Find(id);
            if (entity != null)
            {
                _context.Remove(entity);
                if (_context.SaveChanges() > 0)
                    return 1;
            }
            return 0;
        }

        public async Task<Category> GetAsync(int id, bool useDTO = true)
        {
            if (useDTO)
                return await _context.Categories.FindAsync(id);
            else
            {
                var entity = await _context.Categories
                      .FirstOrDefaultAsync(a => a.Id == id);
                return entity!;
            }
        }

        public async Task<List<Category>> GetsAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> UpdateAsync(Category dto)
        {
            if (CheckExists(dto.Id))
            {
                _context.Entry(dto).State = EntityState.Modified;
                if (await _context.SaveChangesAsync() > 0)
                    return dto;
            }
            return null!;
        }
        public bool CheckExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
