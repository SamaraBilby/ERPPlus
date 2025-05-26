using ERPPlus.Application.DTOs;
using ERPPlus.Application.Services.Interfaces;
using ERPPlus.Application.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ERPPlus.WebAPI.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAll([FromQuery] int page = 1, int pageSize = 10)
        {
            var (items, total) = await _productService.GetPageAsync(page, pageSize);
            Response.Headers.Append("X-Total-Count", total.ToString());

            return Ok(items);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetById(Guid id)
        {
            var product = await _productService.GetByIdAsync(id);

            if(product is null)
                throw new NotFoundException("Produto não encontrado.");


            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> Create([FromBody] CreateProductDTO dto)
        {
            var created = await _productService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { created.Id }, created);
        }

        [HttpPut]
        public async Task<IActionResult> Updete([FromBody] ProductDTO dto)
        {
            var updated = await _productService.UpdateAsync(dto);

            if(!updated)
                throw new NotFoundException("Produto não encontrado.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await _productService.DeleteAsync(id);

            if(!deleted)
                throw new NotFoundException("Produto não encontrado.");

            return Ok();
        }

    }
}
