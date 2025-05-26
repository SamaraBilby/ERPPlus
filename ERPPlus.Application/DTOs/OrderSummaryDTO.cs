
namespace ERPPlus.Application.DTOs
{
    public class OrderSummaryDTO
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public decimal ValueTotal { get; set; }

        public List<ProductOrderSummaryDTO> ProductOrders { get; set; } = new();
    }
}
