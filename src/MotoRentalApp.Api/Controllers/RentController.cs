using Microsoft.AspNetCore.Mvc;
using MotoRentalApp.Application.Interfaces.Services;
using MotoRentalApp.Application.Models;

namespace MotoRentalApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentController : ControllerBase
    {
        private readonly IRentService _rentService;

        public RentController(IRentService rentService)
        {
            _rentService = rentService;
        }

        // GET: api/rents
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _rentService.GetAllRentsAsync();
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.ErrorMessage);
        }

        // GET: api/rents/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _rentService.GetRentByIdAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result.Data);
            }
            return NotFound(result.ErrorMessage);
        }

        // POST: api/rents
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] RentDto rentDto)
        {
            if (rentDto == null)
            {
                return BadRequest("Invalid data.");
            }

            var result = await _rentService.CreateRentAsync(rentDto);
            if (result.IsSuccess)
            {
                return CreatedAtAction(nameof(GetByIdAsync), new { id = rentDto.Id }, rentDto);
            }
            return BadRequest(result.ErrorMessage);
        }
    }
}
