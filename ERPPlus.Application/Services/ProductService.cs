using ERPPlus.Application.DTOs;
using ERPPlus.Application.Mappers;
using ERPPlus.Application.Services.Interfaces;
using ERPPlus.Domain.Interfaces.Repositories;

namespace ERPPlus.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return products.Select(p => p.ToProductDTO());
        }

        public async Task<ProductDTO> GetByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if(product is null)
                return null;

            return product.ToProductDTO();
        }

        public async Task<ProductDTO> CreateAsync(CreateProductDTO createProductDTO)
        {
            var entity = createProductDTO.ToProductEntity();
            await _productRepository.AddAsync(entity);
            return entity.ToProductDTO();
        }

        public async Task<bool> UpdateAsync(ProductDTO dto)
        {
            var product = await _productRepository.GetByIdAsync(dto.Id);

            if(product is null) 
                return false;
            
            product.UpdateProductFromDTO(dto);

            await _productRepository.UpdateAsync(product);
            return true;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if(product is null) 
                return false;

            await _productRepository.DeleteAsync(product);
            return true;
        }

        public async Task<(IEnumerable<ProductDTO> Items, int TotalItems)> GetPageAsync(int page, int pageSize)
        {
            var products = await _productRepository.GetPagedAsync(page,pageSize);
            var totalItems = await _productRepository.GetTotalCountAsync();

            return (products.Select(p => p.ToProductDTO()), totalItems);
        }

    }
}
