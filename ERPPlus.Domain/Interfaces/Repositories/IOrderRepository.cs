using ERPPlus.Domain.Entities;


namespace ERPPlus.Domain.Interfaces.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetPagedAsync(int page, int pageSize);
        Task<int> GetTotalCountAsync();
        Task<Order> GetByIdAsync(Guid id);
        Task AddAsync(Order order);
        Task DeleteAsync(Order order);
    }
}
