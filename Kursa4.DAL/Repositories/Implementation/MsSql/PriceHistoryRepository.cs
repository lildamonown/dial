using Kursa4.DAL.Entities;
using Kursa4.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kursa4.DAL.Repositories.Implementation.EF
{
    public class PriceHistoryRepository : IPriceHistoryRepository
    {
        private readonly ApplicationDbContext _context;

        public PriceHistoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PriceHistory> CreateAsync(PriceHistory entity)
        {
            await _context.PriceHistories.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<PriceHistory>> GetAllAsync()
        {
            return await _context.PriceHistories
                .AsNoTracking()
                .Include(ph => ph.Subservice)
                .ToListAsync();
        }

        public async Task<PriceHistory> GetByIdAsync(int id)
        {
            return await _context.PriceHistories
                .AsNoTracking()
                .Include(ph => ph.Subservice)
                .FirstOrDefaultAsync(ph => ph.Id == id);
        }

        public async Task<PriceHistory> UpdateAsync(PriceHistory entity)
        {
            _context.PriceHistories.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<PriceHistory>> GetBySubserviceIdAsync(int subserviceId)
        {
            return await _context.PriceHistories
                .AsNoTracking()
                .Where(ph => ph.SubserviceId == subserviceId)
                .OrderByDescending(ph => ph.ChangedAt)
                .ToListAsync();
        }
    }
}
