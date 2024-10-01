using APITeaching_172100251.Data;
using APITeaching_172100251.Models;
using Microsoft.EntityFrameworkCore;

namespace APITeaching_172100251.Repositories.Simple
{
    public interface ICustomerService
    {
        Task<List<Customer>> GetsAsync();
        Task<Customer> GetAsync(int id, bool useDTO = true);
        Task<Customer> CreateAsync(Customer dto);
        Task<Customer> UpdateAsync(Customer dto);
        int Delete(int id);
        bool CheckExists(int id);
    }

    public class CustomerService : ICustomerService
    {
        private readonly ApiteachingContext _context;
        public CustomerService(ApiteachingContext context)
        {
            _context = context;
        }
        public async Task<Customer> CreateAsync(Customer dto)
        {
            _context.Customers.Add(dto);
            if (await _context.SaveChangesAsync() > 0)
                return dto;

            return null!;
        }

        public int Delete(int id)
        {
            var entity = _context.Customers.Find(id);
            if (entity != null)
            {
                _context.Remove(entity);
                if (_context.SaveChanges() > 0)
                    return 1;
            }
            return 0;
        }

        public async Task<Customer> GetAsync(int id, bool useDTO = true)
        {
            if (useDTO)
                return await _context.Customers.FindAsync(id);
            else
            {
                var entity = await _context.Customers
                      .FirstOrDefaultAsync(a => a.Id == id);
                return entity!;
            }
        }

        public async Task<List<Customer>> GetsAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<Customer> UpdateAsync(Customer dto)
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
            return _context.Customers.Any(e => e.Id == id);
        }
    }
}
