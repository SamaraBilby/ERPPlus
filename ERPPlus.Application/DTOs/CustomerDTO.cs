
namespace ERPPlus.Application.DTOs
{
    public class CustomerDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<OrderDTO> Orders { get; set; } = new();
    }
}
