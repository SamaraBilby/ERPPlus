
namespace ERPPlus.Application.DTOs
{
    public class ProductOrderSummaryDTO
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal PriceTotal {  get; set; }
    }
}
