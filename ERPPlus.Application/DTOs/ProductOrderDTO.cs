
namespace ERPPlus.Application.DTOs
{
    public class ProductOrderDTO
    {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal PriceTotal { get; set; }
    }
}
