using Microsoft.AspNetCore.Http;
using MotoRentalApp.Application.Common;
using MotoRentalApp.Application.Models;
using MotoRentalApp.Domain.Entities;

namespace MotoRentalApp.Application.Interfaces.Services
{
    public interface IDeliveryPersonService
    {
        Task<Result> RegisterDeliveryPersonAsync(DeliveryPersonDto deliveryPersonDto);
        Task<Result<List<DeliveryPersonDto>>> GetDeliveryPersonsAsync(string cnpj = null, string cnh = null);
        Task<Result> CheckCnpjAsync(string cnpj);
        Task<Result> CheckCnhAsync(string cnh);
        Task<Result> UpdateCnhImageAsync(int deliveryPersonId, IFormFile cnhImage);
        Task<Result<Rent>> RentMotorcycleAsync(int deliveryPersonId, RentRequestDTO rentRequest);
    }
}