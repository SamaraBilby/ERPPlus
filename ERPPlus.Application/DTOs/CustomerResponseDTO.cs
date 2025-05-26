
namespace ERPPlus.Application.DTOs
{
    public class CustomerResponseDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public List<OrderSummaryDTO> Orders { get; set; } = new();
    }
}
