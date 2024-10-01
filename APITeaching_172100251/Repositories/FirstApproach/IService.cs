using APITeaching_172100251.Data;
using APITeaching_172100251.Models;
using Microsoft.EntityFrameworkCore;

namespace APITeaching_172100251.Repositories.FirstApproach
{
    public interface IService<T> where T : class
    {
        Task<T> GetAsync(int id, bool useDTO = true);
        Task<IEnumerable<T>> GetsAsync();
        Task<T> CreateAsync(T dto);
        int DeleteAsync(int id);
        Task<T> UpdateAsync(T dto);
        bool CheckExists(int id);
    }
    public class CountryService : IService<Country>
    {
        private readonly ApiteachingContext _context;
        public CountryService(ApiteachingContext context)
        {
            _context = context;
        }

        public bool CheckExists(int id)
        {
            return _context.Countries.Any(e => e.Id == id);
        }

        public async Task<Country> CreateAsync(Country dto)
        {
            _context.Countries.Add(dto);
            if (await _context.SaveChangesAsync() > 0)
                return dto;

            return null!;
        }

        public int DeleteAsync(int id)
        {

            var entity = _context.Countries.Find(id);
            if (entity != null)
            {
                _context.Remove(entity);
                if (_context.SaveChanges() > 0)
                    return 1;
            }
            return 0;
        }

        public async Task<Country> GetAsync(int id, bool useDTO = true)
        {
            if (useDTO)
                return await _context.Countries.FindAsync(id);
            else
            {
                var entity = await _context.Countries
                      .FirstOrDefaultAsync(a => a.Id == id);
                return entity!;
            }
        }

        public async Task<IEnumerable<Country>> GetsAsync()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<Country> UpdateAsync(Country dto)
        {
            if (CheckExists(dto.Id))
            {
                _context.Entry(dto).State = EntityState.Modified;
                if (await _context.SaveChangesAsync() > 0)
                    return dto;
            }
            return null!;
        }
    }
    public class ProvinceService : IService<Province>
    {
        private readonly ApiteachingContext _context;
        public ProvinceService(ApiteachingContext context)
        {
            _context = context;
        }

        public bool CheckExists(int id)
        {
            return _context.Countries.Any(e => e.Id == id);
        }

        public async Task<Province> CreateAsync(Province dto)
        {
            _context.Provinces.Add(dto);
            if (await _context.SaveChangesAsync() > 0)
                return dto;

            return null!;
        }

        public int DeleteAsync(int id)
        {

            var entity = _context.Provinces.Find(id);
            if (entity != null)
            {
                _context.Remove(entity);
                if (_context.SaveChanges() > 0)
                    return 1;
            }
            return 0;
        }

        public async Task<Province> GetAsync(int id, bool useDTO = true)
        {
            if (useDTO)
                return await _context.Provinces.FindAsync(id);
            else
            {
                var entity = await _context.Provinces
                      .FirstOrDefaultAsync(a => a.Id == id);
                return entity!;
            }
        }

        public async Task<IEnumerable<Province>> GetsAsync()
        {
            return await _context.Provinces.ToListAsync();
        }

        public async Task<Province> UpdateAsync(Province dto)
        {
            if (CheckExists(dto.Id))
            {
                _context.Entry(dto).State = EntityState.Modified;
                if (await _context.SaveChangesAsync() > 0)
                    return dto;
            }
            return null!;
        }
    }
    public class DistrictService : IService<District>
    {
        private readonly ApiteachingContext _context;
        public DistrictService(ApiteachingContext context)
        {
            _context = context;
        }

        public bool CheckExists(int id)
        {
            return _context.Districts.Any(e => e.Id == id);
        }

        public async Task<District> CreateAsync(District dto)
        {
            _context.Districts.Add(dto);
            if (await _context.SaveChangesAsync() > 0)
                return dto;

            return null!;
        }

        public int DeleteAsync(int id)
        {

            var entity = _context.Districts.Find(id);
            if (entity != null)
            {
                _context.Remove(entity);
                if (_context.SaveChanges() > 0)
                    return 1;
            }
            return 0;
        }

        public async Task<District> GetAsync(int id, bool useDTO = true)
        {
            if (useDTO)
                return await _context.Districts.FindAsync(id);
            else
            {
                var entity = await _context.Districts
                      .FirstOrDefaultAsync(a => a.Id == id);
                return entity!;
            }
        }

        public async Task<IEnumerable<District>> GetsAsync()
        {
            return await _context.Districts.ToListAsync();
        }

        public async Task<District> UpdateAsync(District dto)
        {
            if (CheckExists(dto.Id))
            {
                _context.Entry(dto).State = EntityState.Modified;
                if (await _context.SaveChangesAsync() > 0)
                    return dto;
            }
            return null!;
        }
    }
    public class WardService : IService<Ward>
    {
        private readonly ApiteachingContext _context;
        public WardService(ApiteachingContext context)
        {
            _context = context;
        }

        public bool CheckExists(int id)
        {
            return _context.Countries.Any(e => e.Id == id);
        }

        public async Task<Ward> CreateAsync(Ward dto)
        {
            _context.Wards.Add(dto);
            if (await _context.SaveChangesAsync() > 0)
                return dto;

            return null!;
        }

        public int DeleteAsync(int id)
        {

            var entity = _context.Wards.Find(id);
            if (entity != null)
            {
                _context.Remove(entity);
                if (_context.SaveChanges() > 0)
                    return 1;
            }
            return 0;
        }

        public async Task<Ward> GetAsync(int id, bool useDTO = true)
        {
            if (useDTO)
                return await _context.Wards.FindAsync(id);
            else
            {
                var entity = await _context.Wards
                      .FirstOrDefaultAsync(a => a.Id == id);
                return entity!;
            }
        }

        public async Task<IEnumerable<Ward>> GetsAsync()
        {
            return await _context.Wards.ToListAsync();
        }

        public async Task<Ward> UpdateAsync(Ward dto)
        {
            if (CheckExists(dto.Id))
            {
                _context.Entry(dto).State = EntityState.Modified;
                if (await _context.SaveChangesAsync() > 0)
                    return dto;
            }
            return null!;
        }
    }
    public class AddressService : IService<Address>
    {
        private readonly ApiteachingContext _context;
        public AddressService(ApiteachingContext context)
        {
            _context = context;
        }

        public bool CheckExists(int id)
        {
            return _context.Countries.Any(e => e.Id == id);
        }

        public async Task<Address> CreateAsync(Address dto)
        {
            _context.Addresses.Add(dto);
            if (await _context.SaveChangesAsync() > 0)
                return dto;

            return null!;
        }

        public int DeleteAsync(int id)
        {

            var entity = _context.Addresses.Find(id);
            if (entity != null)
            {
                _context.Remove(entity);
                if (_context.SaveChanges() > 0)
                    return 1;
            }
            return 0;
        }

        public async Task<Address> GetAsync(int id, bool useDTO = true)
        {
            if (useDTO)
                return await _context.Addresses.FindAsync(id);
            else
            {
                var entity = await _context.Addresses
                    .Include(c => c.Country)
                      .Include(p => p.Province)
                      .Include(d => d.District)
                      .Include(w => w.Ward)
                      .FirstOrDefaultAsync(a => a.Id == id);
                return entity!;
            }
        }

        public async Task<IEnumerable<Address>> GetsAsync()
        {
            return await _context.Addresses.ToListAsync();
        }

        public async Task<Address> UpdateAsync(Address dto)
        {
            if (CheckExists(dto.Id))
            {
                _context.Entry(dto).State = EntityState.Modified;
                if (await _context.SaveChangesAsync() > 0)
                    return dto;
            }
            return null!;
        }
    }
}
