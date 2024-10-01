using APITeaching_172100251.Data;
using APITeaching_172100251.Models;
using Microsoft.EntityFrameworkCore;

namespace APITeaching_172100251.Repositories.Simple
{
    public interface IOrderService
    {
        Task<List<Order>> GetsAsync();
        Task<Order> GetAsync(int id, bool useDTO = true);
        Task<Order> CreateAsync(Order dto);
        Task<Order> UpdateAsync(Order dto);
        int Delete(int id);
        bool CheckExists(int id);
    }

    public class OrderService : IOrderService
    {
        private readonly ApiteachingContext _context;
        public OrderService(ApiteachingContext context)
        {
            _context = context;
        }
        public async Task<Order> CreateAsync(Order dto)
        {
            _context.Orders.Add(dto);
            if (await _context.SaveChangesAsync() > 0)
                return dto;

            return null!;
        }

        public int Delete(int id)
        {
            var entity = _context.Orders.Find(id);
            if (entity != null)
            {
                _context.Remove(entity);
                if (_context.SaveChanges() > 0)
                    return 1;
            }
            return 0;
        }

        public async Task<Order> GetAsync(int id, bool useDTO = true)
        {
            if (useDTO)
                return await _context.Orders.FindAsync(id);
            else
            {
                var entity = await _context.Orders
                      .Include(c => c.Customer)
                      .Include(e => e.Employee)
                      .Include(s => s.Ship)
                      .FirstOrDefaultAsync(a => a.Id == id);
                return entity!;
            }
        }

        public async Task<List<Order>> GetsAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> UpdateAsync(Order dto)
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
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
