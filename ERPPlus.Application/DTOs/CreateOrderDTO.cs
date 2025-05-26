
namespace ERPPlus.Application.DTOs
{
    public class CreateOrderDTO
    {
        public Guid CustomerId { get; set; }
        public List<ProductOrderDTO> ProductOrders { get; set; } = new();
    }
}
