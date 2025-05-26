
namespace ERPPlus.Application.DTOs
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public decimal ValueTotal { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }

        public List<ProductOrderSummaryDTO> ProductOrders { get; set; } = new();
    }
}
