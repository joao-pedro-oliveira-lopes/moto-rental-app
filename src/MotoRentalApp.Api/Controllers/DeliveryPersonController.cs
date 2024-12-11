using Microsoft.AspNetCore.Mvc;
using MotoRentalApp.Application.Interfaces.Services;
using MotoRentalApp.Application.Models;

namespace MotoRentalApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryPersonController : ControllerBase
    {
        private readonly IDeliveryPersonService _deliveryPersonService;

        public DeliveryPersonController(IDeliveryPersonService deliveryPersonService)
        {
            _deliveryPersonService = deliveryPersonService;
        }

        
        // Endpoint para cadastrar entregador
        [HttpPost]
        public async Task<IActionResult> RegisterDeliveryPerson([FromBody] DeliveryPersonDto deliveryPersonDto)
        {
            var result = await _deliveryPersonService.RegisterDeliveryPersonAsync(deliveryPersonDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);  
            }

            return Ok(result);
        }

        // Endpoint para consultar entregadores
        [HttpGet]
        public async Task<IActionResult> GetDeliveryPersons([FromQuery] string cnpj = null, [FromQuery] string cnh = null)
        {
            var result = await _deliveryPersonService.GetDeliveryPersonsAsync(cnpj, cnh);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);  
            }

            return Ok(result.Data);  
        }

        // Endpoint para verificar se o CNPJ já existe
        [HttpGet("check-cnpj")]
        public async Task<IActionResult> CheckCnpj([FromQuery] string cnpj)
        {
            var result = await _deliveryPersonService.CheckCnpjAsync(cnpj);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage); 
            }

            return Ok(result);  
        }

        // Endpoint para verificar se a CNH já existe
        [HttpGet("check-cnh")]
        public async Task<IActionResult> CheckCnh([FromQuery] string cnh)
        {
            var result = await _deliveryPersonService.CheckCnhAsync(cnh);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);  
            }

            return Ok(result);  
        }

        [HttpPost("update-cnh/{deliveryPersonId}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateCnhImage([FromForm] int deliveryPersonId, IFormFile cnhImage)
        {
            if (cnhImage == null)
                return BadRequest("Imagem CNH não fornecida.");

            var result = await _deliveryPersonService.UpdateCnhImageAsync(deliveryPersonId, cnhImage);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok("Imagem CNH atualizada com sucesso.");
        }

        [HttpPost("{deliveryPersonId}/rent")]
        public async Task<IActionResult> RentMotorcycle(int deliveryPersonId, [FromBody] RentRequestDTO rentRequestDTO)
        {
            try
            {
                var rent = await _deliveryPersonService.RentMotorcycleAsync(deliveryPersonId, rentRequestDTO);
                return Ok(rent);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
