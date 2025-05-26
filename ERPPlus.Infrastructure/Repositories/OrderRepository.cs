using ERPPlus.Domain.Entities;
using ERPPlus.Domain.Interfaces.Repositories;
using ERPPlus.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPPlus.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ERPPlusDbContext _context;
        public OrderRepository(ERPPlusDbContext context) 
        {
        
            _context = context;
        
        }

        public async Task<IEnumerable<Order>> GetPagedAsync(int page, int pageSize)
        {
            return await _context.Orders.Include(o => o.Customer)
                .Include(o => o.ProductOrders)
                .ThenInclude(po => po.Product)
                .OrderByDescending(o => o.Date)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.Orders.CountAsync();
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
            return await _context.Orders.Include(o => o.Customer)
                .Include(o => o.ProductOrders)
                .ThenInclude(po => po.Product)
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Order order)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }
}
