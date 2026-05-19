using Kursa4.DAL.Entities;
using Kursa4.DAL.Exceptions;
using Kursa4.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kursa4.DAL.Repositories.Implementation.EF
{
    public class SubserviceRepository : ISubserviceRepository
    {
        private readonly ApplicationDbContext _context;

        public SubserviceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Subservice> CreateAsync(Subservice entity)
        {
            await _context.Subservices.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Subservice>> GetAllAsync()
        {
            return await _context.Subservices
                .AsNoTracking()
                .Include(s => s.Service)
                .ToListAsync();
        }

        public async Task<List<Subservice>> GetAllByIdOrderAsync(int id)
        {
            return await _context.Subservices
                .AsNoTracking()
                .Where(s => s.Orders.Any(o => o.Id == id))
                .Include(s => s.Service)
                .ToListAsync();
        }

        public async Task<List<Subservice>> GetAllByIdServiceAsync(int id)
        {
            return await _context.Subservices
                .AsNoTracking()
                .Where(s => s.ServiceId == id)
                .Include(s => s.Service)
                .ToListAsync();
        }

        public async Task<List<Subservice>> GetAllVisibleAsync()
        {
            return await _context.Subservices
                .AsNoTracking()
                .Where(s => s.Visible)
                .Include(s => s.Service)
                .ToListAsync();
        }

        public async Task<Subservice> GetByIdAsync(int id)
        {
            return await _context.Subservices
                .AsNoTracking()
                .Include(s => s.Service)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Subservice> UpdateAsync(Subservice entity)
        {
            var foundEntity = await _context.Subservices
                            .AsNoTracking()
                            .FirstOrDefaultAsync(c => c.Id == entity.Id);

            if (foundEntity is null)
            {
                throw new NotFoundException(nameof(entity), entity.Id);
            }

            _context.Subservices.Update(entity);

            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
