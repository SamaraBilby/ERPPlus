using ERPPlus.Domain.Entities;

namespace ERPPlus.Domain.Interfaces.Repositories
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetByIdAsync(Guid id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(Product product);
        Task<IEnumerable<Product>> GetPagedAsync(int page, int pageSize);
        Task<int> GetTotalCountAsync();
    }
}
