using Kursa4.DAL.Entities;
using Kursa4.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kursa4.DAL.Repositories.Implementation.EF
{
    public class StatisticRepository : IStatisticRepository
    {
        private readonly ApplicationDbContext _context;

        public StatisticRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Statistic>> GetStatisticsAsync(DateTime date)
        {
            var statistics = await _context.Reports
                .Where(r => r.DateCompleted.Month == date.Month && r.DateCompleted.Year == date.Year)
                .Join(_context.Orders,
                    r => r.OrderId,
                    o => o.Id,
                    (r, o) => o)
                .SelectMany(o => o.Subservices)
                .GroupBy(s => s.Name)
                .Select(g => new Statistic
                {
                    NameSubservice = g.Key,
                    Count = g.Count()
                })
                .OrderByDescending(s => s.Count)
                .ToListAsync();

            return statistics;
        }
    }
}
