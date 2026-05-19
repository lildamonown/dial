using Kursa4.DAL.Entities;
using Kursa4.DAL.Exceptions;
using Kursa4.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kursa4.DAL.Repositories.Implementation.EF
{
    public class StatusRepository : IStatusRepository
    {
        private readonly ApplicationDbContext _context;

        public async Task<Status> CreateAsync(Status entity)
        {
            await _context.Statuses.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public StatusRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Status>> GetAllAsync()
        {
            return await _context.Statuses
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Status> GetByIdAsync(int id)
        {
            return await _context.Statuses
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Status> UpdateAsync(Status entity)
        {
            var foundEntity = await _context.Statuses
                            .AsNoTracking()
                            .FirstOrDefaultAsync(c => c.Id == entity.Id);

            if (foundEntity is null)
            {
                throw new NotFoundException(nameof(entity), entity.Id);
            }

            _context.Statuses.Update(entity);

            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<Status> GetByNameAsync(string name)
        {
            return await _context.Statuses
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Name == name);
        }
    }
}
