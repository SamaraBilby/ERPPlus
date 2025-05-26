using ERPPlus.Application.DTOs;

namespace ERPPlus.BlazorUI.Services
{
    public class CustomerService
    {
        private readonly HttpClient _httpClient;

        public CustomerService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ERPPlusAPI");
        }

        public async Task<List<CustomerResponseDTO>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<CustomerResponseDTO>>("customer");
        }

        public async Task<CustomerResponseDTO> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<CustomerResponseDTO>($"customer/{id}");
        }

        public async Task<CustomerResponseDTO> CreateAsync(CreateCustomerDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync("customer", dto);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<CustomerResponseDTO>();

            var content = await response.Content.ReadAsStringAsync();
            throw new Exception($"Erro ao criar cliente: {content}");
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"customer/{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(UpdateCustomerDTO dto)
        {
            var response = await _httpClient.PutAsJsonAsync("customer", dto);
            return response.IsSuccessStatusCode;
        }
    }
}
