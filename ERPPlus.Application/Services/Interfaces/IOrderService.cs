using ERPPlus.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERPPlus.Application.Services.Interfaces
{
    public interface IOrderService
    {
        Task<(IEnumerable<OrderDTO> Items, int TotalItems)> GetPagedAsync(int page, int pageSize);
        Task<OrderDTO> GetByIdAsync(Guid id);
        Task<OrderDTO> CreateAsync(CreateOrderDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}
