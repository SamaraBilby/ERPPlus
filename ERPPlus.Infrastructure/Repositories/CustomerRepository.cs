using ERPPlus.Domain.Entities;
using ERPPlus.Domain.Interfaces.Repositories;
using ERPPlus.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ERPPlus.Infrastructure.Repositories
{
    public class CustomerRepository :ICustomerRepository
    {
        private readonly ERPPlusDbContext _context;

        public CustomerRepository( ERPPlusDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers.Include(c => c.Orders)
                .ThenInclude(o => o.ProductOrders)
                .ThenInclude(po => po.Product)
                .AsNoTracking().ToListAsync();
        }

        public async Task<Customer?> GetByIdAsync(Guid id)
        {
            return await _context.Customers.Include(c => c.Orders)
                .ThenInclude(o => o.ProductOrders)
                .ThenInclude(po => po.Product)
                .AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Customer customer)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
    }
}
