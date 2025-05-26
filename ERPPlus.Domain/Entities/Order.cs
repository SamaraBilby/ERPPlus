using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPPlus.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public decimal ValueTotal { get; set; }
        public Guid CustomerId { get; set; }

        public Customer? Customer { get; set; }

        public ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();
    }
}
