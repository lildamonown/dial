using Microsoft.EntityFrameworkCore;
using Kursa4.DAL.Entities;
using Kursa4.DAL.Repositories.Interfaces;

namespace Kursa4.DAL.Repositories.Implementation.EF
{
    public class CarBrandRepository : ICarBrandRepository
    {
        private readonly ApplicationDbContext _context;

        public CarBrandRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CarBrand> CreateAsync(CarBrand item)
        {
            await _context.CarBrands.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<CarBrand> GetByIdAsync(int id)
        {
            return await _context.CarBrands.FindAsync(id);
        }

        public async Task<List<CarBrand>> GetAllAsync()
        {
            return await _context.CarBrands.ToListAsync();
        }

        public async Task<CarBrand> UpdateAsync(CarBrand item)
        {
            _context.CarBrands.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }
    }
}
