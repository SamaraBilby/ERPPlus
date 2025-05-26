using ERPPlus.Application.DTOs;
using System.Net.Http.Json;

namespace ERPPlus.BlazorUI.Services
{
    public class OrderService
    {
        private readonly HttpClient _httpClient;

        public OrderService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ERPPlusAPI");
        }

        public class PaginatedResult<T>
        {
            public List<T> Items { get; set; } = new();
            public int Total { get; set; }
        }

        public async Task<PaginatedResult<OrderDTO>> GetPagedAsync(int page, int pageSize)
        {
            var response = await _httpClient.GetAsync($"order?page={page}&pageSize={pageSize}");
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                throw new Exception($"Erro ao buscar pedidos: {content}");
            }

            var orders = await response.Content.ReadFromJsonAsync<List<OrderDTO>>();
            response.Headers.TryGetValues("X-Total-Count", out var totalValues);
            var total = totalValues?.FirstOrDefault() != null ? int.Parse(totalValues.First()) : 0;

            return new PaginatedResult<OrderDTO>
            {
                Items = orders ?? new List<OrderDTO>(),
                Total = total
            };
        }

        public async Task<OrderDTO> GetByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<OrderDTO>($"order/{id}");
        }

        public async Task<OrderDTO> CreateAsync(CreateOrderDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync("order", dto);
            if (response.IsSuccessStatusCode)
                return await response.Content.ReadFromJsonAsync<OrderDTO>();

            var content = await response.Content.ReadAsStringAsync();
            throw new Exception($"Erro ao criar pedido: {content}");
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"order?id={id}");
            return response.IsSuccessStatusCode;
        }
    }
}
