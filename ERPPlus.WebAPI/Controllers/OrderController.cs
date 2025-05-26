using ERPPlus.Application.DTOs;
using ERPPlus.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ERPPlus.WebAPI.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
       private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDTO>>> GetAll([FromQuery] int page =1, int pageSize = 10)
        {
            var (items, total) = await _orderService.GetPagedAsync(page, pageSize);
            Response.Headers.Append("X-Total-Count", total.ToString());
            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetById(Guid id)
        {
            var order = await _orderService.GetByIdAsync(id);
            return Ok(order);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDTO>> Create([FromBody] CreateOrderDTO dto)
        {
            var created = await _orderService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById),new {id = created.Id },created);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _orderService.DeleteAsync(id);
            return Ok(deleted);
        }
    }
}
