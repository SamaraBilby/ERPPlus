using ERPPlus.Application.DTOs;
using ERPPlus.Application.Exceptions;
using ERPPlus.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ERPPlus.WebAPI.Controllers
{
    [ApiController]
    [Route("api/customer")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerResponseDTO>>> GetAll()
        {
            var cutomers = await _customerService.GetAllAsync();
            return Ok(cutomers);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResponseDTO>> GetById(Guid id)
        {
            var customer = await _customerService.GetByIdAsync(id);

            if(customer == null) 
                throw new NotFoundException("Cliente não encotrado");

            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerDTO>> Create([FromBody] CreateCustomerDTO dto)
        {
            var created = await _customerService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new {id = created.Id}, created);
        }

        [HttpPut]
        public async Task<IActionResult> Update(UpdateCustomerDTO dto)
        {
            var upadte = await _customerService.UpdateAsync(dto);

            if(!upadte)
                throw new NotFoundException("Não foi possível atualizar o cadastro. Cliente não encontrado");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id) {
        
            var delete = await _customerService.DeleteAsync(id);

            if (!delete)
                throw new NotFoundException("Não foi possível excluir o cliente. Cliente não encontrado");

            return Ok();
        }



    }
}
