using ERPPlus.Application.DTOs;
using System.Net.Http.Json;

namespace ERPPlus.BlazorUI.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ERPPlusAPI");
        }

        public async Task<List<ProductDTO>> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<ProductDTO>>("product");
        }

        public async Task<ProductDTO?> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<ProductDTO>($"product/{id}");
        }

        public async Task<ProductDTO?> CreateAsync(CreateProductDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync("product", dto);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<ProductDTO>();

            var content = await response.Content.ReadAsStringAsync();
            throw new Exception($"Erro ao criar produto: {content}");
        }

        public async Task<bool> UpdateAsync(ProductDTO dto)
        {
            var response = await _httpClient.PutAsJsonAsync("product", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"product/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
