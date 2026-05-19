using Kursa4.DAL.Entities;
using Kursa4.DAL.Exceptions;
using Kursa4.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kursa4.DAL.Repositories.Implementation.EF
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _context;

        public CarRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Car> CreateAsync(Car entity)
        {
            await _context.Cars.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Car> UpdateAsync(Car entity)
        {
            var foundEntity = await _context.Cars
                            .AsNoTracking()
                            .FirstOrDefaultAsync(c => c.Id == entity.Id);

            if (foundEntity is null)
            {
                throw new NotFoundException(nameof(entity), entity.Id);
            }

            _context.Cars.Update(entity);

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Car> GetByIdAsync(int id)
        {
            return await _context.Cars
                .AsNoTracking()
                .FirstOrDefaultAsync(car => car.Id == id);
        }

        public async Task<List<Car>> GetAllAsync()
        {
            return await _context.Cars
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
