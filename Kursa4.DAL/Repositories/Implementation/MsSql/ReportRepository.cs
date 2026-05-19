using Kursa4.DAL.Entities;
using Kursa4.DAL.Exceptions;
using Kursa4.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kursa4.DAL.Repositories.Implementation.EF
{
    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationDbContext _context;

        public ReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Report> CreateAsync(Report entity)
        {
            await _context.Reports.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Report>> GetAllAsync()
        {
            return await _context.Reports
                .AsNoTracking()
                .Include(r => r.Order)
                .ToListAsync();
        }

        public async Task<List<Report>> GetAllByDateAsync(DateTime date)
        {
            return await _context.Reports
                .AsNoTracking()
                .Where(r => r.DateCompleted.Date == date.Date)
                .Include(r => r.Order)
                .ToListAsync();
        }

        public async Task<Report> GetByIdAsync(int id)
        {
            return await _context.Reports
                .AsNoTracking()
                .Include(r => r.Order)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Report> UpdateAsync(Report entity)
        {
            var foundEntity = await _context.Reports
                            .AsNoTracking()
                            .FirstOrDefaultAsync(c => c.Id == entity.Id);

            if (foundEntity is null)
            {
                throw new NotFoundException(nameof(entity), entity.Id);
            }

            _context.Reports.Update(entity);

            await _context.SaveChangesAsync();

            return entity;
        }
    }
}