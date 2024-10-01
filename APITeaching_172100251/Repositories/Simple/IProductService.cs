using APITeaching_172100251.Data;
using APITeaching_172100251.Models;
using Microsoft.EntityFrameworkCore;

namespace APITeaching_172100251.Repositories.Simple
{
    public interface IProductService
    {
        Task<List<Product>> GetsAsync();
        Task<Product> GetAsync(int id, bool useDTO = true);
        Task<Product> CreateAsync(Product dto);
        Task<Product> UpdateAsync(Product dto);
        int Delete(int id);
        bool CheckExists(int id);
    }

    public class ProductService : IProductService
    {
        private readonly ApiteachingContext _context;
        public ProductService(ApiteachingContext context)
        {
            _context = context;
        }
        public async Task<Product> CreateAsync(Product dto)
        {
            _context.Products.Add(dto);
            if (await _context.SaveChangesAsync() > 0)
                return dto;

            return null!;
        }

        public int Delete(int id)
        {
            var entity = _context.Products.Find(id);
            if (entity != null)
            {
                _context.Remove(entity);
                if (_context.SaveChanges() > 0)
                    return 1;
            }
            return 0;
        }

        public async Task<Product> GetAsync(int id, bool useDTO = true)
        {
            if (useDTO)
                return await _context.Products.FindAsync(id);
            else
            {
                var entity = await _context.Products
                      .Include(c => c.Category)
                      .Include(s => s.Supplier)
                      .FirstOrDefaultAsync(a => a.Id == id);
                return entity!;
            }
        }

        public async Task<List<Product>> GetsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> UpdateAsync(Product dto)
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
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
