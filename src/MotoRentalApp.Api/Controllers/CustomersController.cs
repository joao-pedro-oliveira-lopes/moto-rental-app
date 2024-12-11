using Microsoft.AspNetCore.Mvc;
using MotoRentalApp.Application.Models;
using MotoRentalApp.Application.Interfaces.Services;

namespace MotoRentalApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET: api/customers
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _customerService.GetAllCustomersAsync();
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        // GET: api/customers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _customerService.GetCustomerByIdAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return NotFound(result.ErrorMessage);
        }

        // POST: api/customers
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CustomerDto customerDto)
        {
            if (customerDto == null)
            {
                return BadRequest("Invalid data.");
            }

            var result = await _customerService.CreateCustomerAsync(customerDto);
            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(GetByIdAsync), new { id = customerDto.Id }, customerDto);
            }
            return BadRequest(result.ErrorMessage);
        }

    }
}
