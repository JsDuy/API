using APITeaching_172100251.Data;
using APITeaching_172100251.Models;
using Microsoft.EntityFrameworkCore;

namespace APITeaching_172100251.Repositories.Simple
{
    public interface IOrderDetailService
    {
        Task<List<OrderDetail>> GetsAsync();
        Task<OrderDetail> GetAsync(int id, bool useDTO = true);
        Task<OrderDetail> CreateAsync(OrderDetail dto);
        Task<OrderDetail> UpdateAsync(OrderDetail dto);
        int Delete(int id);
        bool CheckExists(int? id);
    }

    public class OrderDetailService : IOrderDetailService
    {
        private readonly ApiteachingContext _context;
        public OrderDetailService(ApiteachingContext context)
        {
            _context = context;
        }
        public async Task<OrderDetail> CreateAsync(OrderDetail dto)
        {
            _context.OrderDetails.Add(dto);
            if (await _context.SaveChangesAsync() > 0)
                return dto;

            return null!;
        }

        public int Delete(int id)
        {
            var entity = _context.OrderDetails.Find(id);
            if (entity != null)
            {
                _context.Remove(entity);
                if (_context.SaveChanges() > 0)
                    return 1;
            }
            return 0;
        }

        public async Task<OrderDetail> GetAsync(int id, bool useDTO = true)
        {
            if (useDTO)
                return await _context.OrderDetails.FindAsync(id);
            else
            {
                var entity = await _context.OrderDetails
                      .Include(p => p.Product)
                      .Include(o => o.Order)
                      .FirstOrDefaultAsync(a => a.Id == id);
                return entity!;
            }
        }

        public async Task<List<OrderDetail>> GetsAsync()
        {
            return await _context.OrderDetails.ToListAsync();
        }

        public async Task<OrderDetail> UpdateAsync(OrderDetail dto)
        {
            if (CheckExists(dto.Id))
            {
                _context.Entry(dto).State = EntityState.Modified;
                if (await _context.SaveChangesAsync() > 0)
                    return dto;
            }
            return null!;
        }
        public bool CheckExists(int? id)
        {
            return _context.OrderDetails.Any(e => e.Id == id);
        }
    }
}
