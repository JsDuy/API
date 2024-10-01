using APITeaching_172100251.Data;
using APITeaching_172100251.Models;
using Microsoft.EntityFrameworkCore;

namespace APITeaching_172100251.Repositories.Simple
{
    public interface ISupplierService
    {
        Task<List<Supplier>> GetsAsync();
        Task<Supplier> GetAsync(int id, bool useDTO = true);
        Task<Supplier> CreateAsync(Supplier dto);
        Task<Supplier> UpdateAsync(Supplier dto);
        int Delete(int id);
        bool CheckExists(int id);
    }

    public class SupplierService : ISupplierService
    {
        private readonly ApiteachingContext _context;
        public SupplierService(ApiteachingContext context)
        {
            _context = context;
        }
        public async Task<Supplier> CreateAsync(Supplier dto)
        {
            _context.Suppliers.Add(dto);
            if (await _context.SaveChangesAsync() > 0)
                return dto;

            return null!;
        }

        public int Delete(int id)
        {
            var entity = _context.Suppliers.Find(id);
            if (entity != null)
            {
                _context.Remove(entity);
                if (_context.SaveChanges() > 0)
                    return 1;
            }
            return 0;
        }

        public async Task<Supplier> GetAsync(int id, bool useDTO = true)
        {
            if (useDTO)
                return await _context.Suppliers.FindAsync(id);
            else
            {
                var entity = await _context.Suppliers
                      .FirstOrDefaultAsync(a => a.Id == id);
                return entity!;
            }
        }

        public async Task<List<Supplier>> GetsAsync()
        {
            return await _context.Suppliers.ToListAsync();
        }

        public async Task<Supplier> UpdateAsync(Supplier dto)
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
            return _context.Suppliers.Any(e => e.Id == id);
        }
    }
}
