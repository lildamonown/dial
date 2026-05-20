using Kursa4.DAL.Entities;
using Kursa4.DAL.Exceptions;
using Kursa4.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kursa4.DAL.Repositories.Implementation.EF
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Order> CreateAsync(Order entity)
        {
            foreach (var subservice in entity.Subservices)
            {
                _context.Subservices.Attach(subservice);
            }

            await _context.Orders.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _context.Orders
                .AsNoTracking()
                .Include(o => o.Status)
                .Include(o => o.User)
                .Include(o => o.Car)
                .Include(o => o.Subservices)
                .OrderByDescending(o => o.CreateAt)
                .ToListAsync();
        }

        public async Task<List<Order>> GetAllByVisiteDateAsync(DateTime date)
        {
            return await _context.Orders
                .AsNoTracking()
                .Where(o => o.VisitDate.HasValue && o.VisitDate.Value.Date == date.Date)
                .Include(o => o.Status)
                .Include(o => o.User)
                .Include(o => o.Car)
                .Include(o => o.Subservices)
                .ToListAsync();
        }

        public async Task<List<Order>> GetAllByIdUserAsync(string userId)
        {
            return await _context.Orders
                .AsNoTracking()
                .Where(o => o.UserId == userId)
                .Include(o => o.Status)
                .Include(o => o.User)
                .Include(o => o.Car)
                .Include(o => o.Subservices)
                .ToListAsync();
        }

        public async Task<List<Order>> GetAllNotConfirmedAsync()
        {
            return await _context.Orders
                .AsNoTracking()
                .Where(o => o.StatusId == 1)
                .Include(o => o.Status)
                .Include(o => o.User)
                .Include(o => o.Car)
                .Include(o => o.Subservices)
                .ToListAsync();
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _context.Orders
                .AsNoTracking()
                .Include(o => o.Status)
                .Include(o => o.User)
                .Include(o => o.Car)
                .Include(o => o.Subservices)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Order> UpdateAsync(Order entity)
        {
            var existingOrder = await _context.Orders
                .Include(o => o.Subservices)
                .FirstOrDefaultAsync(o => o.Id == entity.Id);

            if (existingOrder is null)
                throw new NotFoundException(nameof(entity), entity.Id);

            _context.Entry(existingOrder).CurrentValues.SetValues(entity);

            existingOrder.Subservices.Clear(); 

            foreach (var subservice in entity.Subservices)
            {
                var subserviceToAdd = await _context.Subservices
                    .FirstOrDefaultAsync(s => s.Id == subservice.Id);

                if (subserviceToAdd != null)
                {
                    existingOrder.Subservices.Add(subserviceToAdd);
                }
            }

            await _context.SaveChangesAsync();
            return existingOrder;
        }

        public async Task<List<Order>> GetAllByStatusIdAsync(int id)
        {
            return await _context.Orders
                .AsNoTracking()
                .Where(o => o.StatusId == id)
                .Include(o => o.Status)
                .Include(o => o.User)
                .Include(o => o.Car)
                .Include(o => o.Subservices)
                .ToListAsync();
        }
    }
}