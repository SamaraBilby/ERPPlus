using ERPPlus.Application.DTOs;

namespace ERPPlus.Application.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerResponseDTO>> GetAllAsync();
        Task<CustomerResponseDTO> GetByIdAsync(Guid id);
        Task<CustomerDTO> CreateAsync(CreateCustomerDTO dto);
        Task<bool> UpdateAsync(UpdateCustomerDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
