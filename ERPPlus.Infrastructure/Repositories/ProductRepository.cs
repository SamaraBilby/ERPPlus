using ERPPlus.Domain.Entities;
using ERPPlus.Domain.Interfaces.Repositories;
using ERPPlus.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace ERPPlus.Infrastructure.Repositories
{
    public class ProductRepository: IProductRepository
    {
        private readonly ERPPlusDbContext _context;

        public ProductRepository(ERPPlusDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.AsNoTracking()
                .OrderBy(p => p.Name).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            return await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public  async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetPagedAsync(int page, int pageSize)
        {
            return await _context.Products.AsNoTracking()
                .OrderBy(p => p.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalCountAsync()
        {
            return await _context.Products.CountAsync();
        }

    }
}
