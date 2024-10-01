using APITeaching_172100251.Data;
using APITeaching_172100251.Models;
using Microsoft.EntityFrameworkCore;

namespace APITeaching_172100251.Repositories.Simple
{
    public interface IShipperService
    {
        Task<List<Shipper>> GetsAsync();
        Task<Shipper> GetAsync(int id, bool useDTO = true);
        Task<Shipper> CreateAsync(Shipper dto);
        Task<Shipper> UpdateAsync(Shipper dto);
        int Delete(int id);
        bool CheckExists(int id);
    }
    public class ShipperService : IShipperService
    {
        private readonly ApiteachingContext _context;
        public ShipperService(ApiteachingContext context)
        {
            _context = context;
        }
        public async Task<Shipper> CreateAsync(Shipper dto)
        {
            _context.Shippers.Add(dto);
            if (await _context.SaveChangesAsync() > 0)
                return dto;

            return null!;
        }

        public int Delete(int id)
        {
            var entity = _context.Shippers.Find(id);
            if (entity != null)
            {
                _context.Remove(entity);
                if (_context.SaveChanges() > 0)
                    return 1;
            }
            return 0;
        }

        public async Task<Shipper> GetAsync(int id, bool useDTO = true)
        {
            if (useDTO)
                return await _context.Shippers.FindAsync(id);
            else
            {
                var entity = await _context.Shippers
                      .FirstOrDefaultAsync(a => a.Id == id);
                return entity!;
            }
        }

        public async Task<List<Shipper>> GetsAsync()
        {
            return await _context.Shippers.ToListAsync();
        }

        public async Task<Shipper> UpdateAsync(Shipper dto)
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
