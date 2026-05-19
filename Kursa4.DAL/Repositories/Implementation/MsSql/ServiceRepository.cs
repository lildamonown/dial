using Kursa4.DAL.Entities;
using Kursa4.DAL.Exceptions;
using Kursa4.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kursa4.DAL.Repositories.Implementation.EF
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ApplicationDbContext _context;

        public ServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Service> CreateAsync(Service entity)
        {
            await _context.Services.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Service>> GetAllAsync()
        {
            return await _context.Services
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Service> GetByIdAsync(int id)
        {
            return await _context.Services
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Service> UpdateAsync(Service entity)
        {
            var foundEntity = await _context.Services
                            .AsNoTracking()
                            .FirstOrDefaultAsync(c => c.Id == entity.Id);

            if (foundEntity is null)
            {
                throw new NotFoundException(nameof(entity), entity.Id);
            }

            _context.Services.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }
    }
}
