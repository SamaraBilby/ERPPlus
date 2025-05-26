using ERPPlus.Application.DTOs;
using ERPPlus.Application.Services.Interfaces;
using ERPPlus.Application.Mappers;
using ERPPlus.Domain.Interfaces.Repositories;

namespace ERPPlus.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository) {
        
            _customerRepository = customerRepository;

        }

        public async Task<IEnumerable<CustomerResponseDTO>> GetAllAsync()
        {
            var customers = await _customerRepository.GetAllAsync();

            return customers.Select(c  => c.ToCustomerResponseDTO());
        }

        public async Task<CustomerResponseDTO> GetByIdAsync(Guid id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);

            if (customer == null)
                return null;

            return customer.ToCustomerResponseDTO();
        }

        public async Task<bool> UpdateAsync(UpdateCustomerDTO dto)
        {
            if (dto is null)
                return false;

            var customer = await _customerRepository.GetByIdAsync(dto.Id);

            if (customer is null) 
                return false;

            customer.UpadateCustomerFromDTO(dto);

            await _customerRepository.UpdateAsync(customer);
            return true;
        }

       public async Task<bool> DeleteAsync(Guid id)
        {
            if (id == Guid.Empty)
                return false;

            var customer = await _customerRepository.GetByIdAsync(id);

            if(customer is null)
                return false;


            await _customerRepository.DeleteAsync(customer);
            return true;
        }

        public async Task<CustomerDTO> CreateAsync(CreateCustomerDTO dto)
        {

            if (dto is null)
                return null;

            var entity = dto.ToCustomerEntity();
            await _customerRepository.AddAsync(entity);

            return entity.ToCustomerDTO();
        }

    }
}
