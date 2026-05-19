using Kursa4.DAL.Entities;
using Kursa4.DAL.Exceptions;
using Kursa4.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kursa4.DAL.Repositories.Implementation.EF
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Review> CreateAsync(Review entity)
        {
            await _context.Reviews.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var foundEntity = await _context.Reviews
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);

            if (foundEntity is null)
            {
                throw new NotFoundException("", id);
            }

            _context.Reviews.Remove(foundEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Review>> GetAllAsync()
        {
            return await _context.Reviews
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<float> GetAverageRatingAsync()
        {
            return await _context.Reviews
                    .AverageAsync(r => (float)r.Grade);
        }

        public async Task<Review> GetByIdAsync(int id)
        {
            return await _context.Reviews
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Review> UpdateAsync(Review entity)
        {
            var foundEntity = await _context.Reviews
                            .AsNoTracking()
                            .FirstOrDefaultAsync(c => c.Id == entity.Id);

            if (foundEntity is null)
            {
                throw new NotFoundException(nameof(entity), entity.Id);
            }

            _context.Reviews.Update(entity);

            await _context.SaveChangesAsync();

            return entity;
        }
    }
}