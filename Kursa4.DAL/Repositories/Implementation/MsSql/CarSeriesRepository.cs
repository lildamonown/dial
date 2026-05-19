using Microsoft.EntityFrameworkCore;
using Kursa4.DAL.Entities;
using Kursa4.DAL.Repositories.Interfaces;

namespace Kursa4.DAL.Repositories.Implementation.EF
{
    public class CarSeriesRepository : ICarSeriesRepository
    {
        private readonly ApplicationDbContext _context;

        public CarSeriesRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<CarSeries> CreateAsync(CarSeries item)
        {
            await _context.CarSeries.AddAsync(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<CarSeries> GetByIdAsync(int id)
        {
            return await _context.CarSeries.FindAsync(id);
        }

        public async Task<List<CarSeries>> GetAllAsync()
        {
            return await _context.CarSeries.ToListAsync();
        }

        public async Task<List<CarSeries>> GetAllByBrandIdAsync(int brandId)
        {
            return await _context.CarSeries
                .Where(s => s.CarBrandId == brandId)
                .ToListAsync();
        }

        public async Task<CarSeries> UpdateAsync(CarSeries item)
        {
            _context.CarSeries.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }
    }
}
