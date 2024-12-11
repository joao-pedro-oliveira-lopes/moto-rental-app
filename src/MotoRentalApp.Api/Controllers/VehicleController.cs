using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MotoRentalApp.Application.Interfaces.Services;
using MotoRentalApp.Application.Models;
using MotoRentalApp.Application.Common;

namespace MotoRentalApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        // Método para obter a lista de veículos, somente para admin
        [HttpGet]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<Result<List<VehicleDto>>>> GetVehiclesAsync([FromQuery] string plate = null)
        {
            var result = await _vehicleService.GetVehiclesAsync(plate);

            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            return Ok(result);
        }

        // Método para criar um novo veículo
        [HttpPost]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<Result>> CreateVehicleAsync([FromBody] VehicleDto vehicleDto)
        {
            var result = await _vehicleService.CreateVehicleAsync(vehicleDto);

            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            return Ok(result);
        }

        // Método para atualizar a placa de um veículo
        [HttpPut("{vehicleId}/plate")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<Result>> UpdateVehiclePlateAsync(int vehicleId, [FromBody] string newPlate)
        {
            var result = await _vehicleService.UpdateVehiclePlateAsync(vehicleId, newPlate);

            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            return Ok(result);
        }

        // Método para excluir um veículo
        [HttpDelete("{vehicleId}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<ActionResult<Result>> DeleteVehicleAsync(int vehicleId)
        {
            var result = await _vehicleService.DeleteVehicleAsync(vehicleId);

            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            return Ok(result);
        }

        // Método para obter veículos disponíveis
        [HttpGet("available")]
        public async Task<ActionResult<Result<List<VehicleDto>>>> GetAvailableVehiclesAsync(DateTime startDate, DateTime endDate)
        {
            var result = await _vehicleService.GetAvailableVehiclesAsync(startDate, endDate);

            if (!result.IsSuccess)
                return BadRequest(result.ErrorMessage);

            return Ok(result);
        }
    }
}
