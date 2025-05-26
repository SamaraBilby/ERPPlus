using ERPPlus.Application.DTOs;

namespace ERPPlus.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllAsync();
        Task<ProductDTO> GetByIdAsync(Guid id);
        Task<ProductDTO> CreateAsync(CreateProductDTO dto);
        Task<bool> UpdateAsync(ProductDTO dto);
        Task<bool> DeleteAsync(Guid id);
        Task<(IEnumerable<ProductDTO> Items, int TotalItems)> GetPageAsync(int page, int pageSize);
    }
}
