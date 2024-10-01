using APITeaching_172100251.Data;
using APITeaching_172100251.Models;
using Microsoft.EntityFrameworkCore;

namespace APITeaching_172100251.Repositories.Simple
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetsAsync();
        Task<Employee> GetAsync(int id, bool useDTO = true);
        Task<Employee> CreateAsync(Employee dto);
        Task<Employee> UpdateAsync(Employee dto);
        int Delete(int id);
        bool CheckExists(int id);
    }
    public class EmployeeService : IEmployeeService
    {
        private readonly ApiteachingContext _context;
        public EmployeeService(ApiteachingContext context)
        {
            _context = context;
        }
        public async Task<Employee> CreateAsync(Employee dto)
        {
            _context.Employees.Add(dto);
            if (await _context.SaveChangesAsync() > 0)
                return dto;

            return null!;
        }

        public int Delete(int id)
        {
            var entity = _context.Employees.Find(id);
            if (entity != null)
            {
                _context.Remove(entity);
                if (_context.SaveChanges() > 0)
                    return 1;
            }
            return 0;
        }

        public async Task<Employee> GetAsync(int id, bool useDTO = true)
        {
            if (useDTO)
                return await _context.Employees.FindAsync(id);
            else
            {
                var entity = await _context.Employees
                    .Include(a => a.Account)
                      .FirstOrDefaultAsync(a => a.Id == id);
                return entity!;
            }
        }

        public async Task<List<Employee>> GetsAsync()
        {
            return await _context.Employees.ToListAsync();
        }

        public async Task<Employee> UpdateAsync(Employee dto)
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
            return _context.Employees.Any(e => e.Id == id);
        }
    }
}
