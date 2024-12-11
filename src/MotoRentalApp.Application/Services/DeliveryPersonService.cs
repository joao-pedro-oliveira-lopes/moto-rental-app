using MotoRentalApp.Application.Interfaces.Repositories;
using MotoRentalApp.Application.Interfaces.Services;
using MotoRentalApp.Application.Models;
using MotoRentalApp.Application.Common;
using MotoRentalApp.Domain.Entities;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MotoRentalApp.Domain.Enums;

namespace MotoRentalApp.Application.Services
{
    public class DeliveryPersonService : IDeliveryPersonService
    {
        private readonly IRentRepository _rentRepository;
        private readonly IDeliveryPersonRepository _deliveryPersonRepository;
        private readonly IStorageService _storageService;

        public DeliveryPersonService(IDeliveryPersonRepository deliveryPersonRepository, IStorageService storageService, IRentRepository rentRepository)
        {
            _deliveryPersonRepository = deliveryPersonRepository;
            _storageService = storageService;
            _rentRepository = rentRepository;
        }

        public async Task<Result> UpdateCnhImageAsync(int deliveryPersonId, IFormFile cnhImage)
        {
            var deliveryPerson = await _deliveryPersonRepository.GetByIdAsync(deliveryPersonId);
            if (deliveryPerson == null)
                return Result.Failure("Entregador não encontrado.");

            var imagePath = await _storageService.UploadCnhImageAsync(cnhImage);

            deliveryPerson.CNHImage = imagePath;
            await _deliveryPersonRepository.UpdateAsync(deliveryPerson);

            return Result.Success();
        }

        public async Task<Result> RegisterDeliveryPersonAsync(DeliveryPersonDto deliveryPersonDto)
        {
            
            if (await _deliveryPersonRepository.ExistsByCNPJAsync(deliveryPersonDto.CNPJ))
                return Result.Failure("CNPJ já cadastrado.");

            
            if (await _deliveryPersonRepository.ExistsByCNHAsync(deliveryPersonDto.CNHNumber))
                return Result.Failure("Número da CNH já cadastrado.");

            
            if (deliveryPersonDto.CNHType != "A" && deliveryPersonDto.CNHType != "B" && deliveryPersonDto.CNHType != "A+B")
                return Result.Failure("Tipo de CNH inválido.");

            var deliveryPerson = new DeliveryPerson
            {
                Name = deliveryPersonDto.Name,
                CNPJ = deliveryPersonDto.CNPJ,
                BirthDate = deliveryPersonDto.BirthDate,
                CNHNumber = deliveryPersonDto.CNHNumber,
                CNHType = deliveryPersonDto.CNHType,
                CNHImage = deliveryPersonDto.CNHImage
            };

            await _deliveryPersonRepository.AddAsync(deliveryPerson);

            return Result.Success();
        }


        public async Task<Result<List<DeliveryPersonDto>>> GetDeliveryPersonsAsync(string cnpj = null, string cnh = null)
        {
            var deliveryPersons = await _deliveryPersonRepository.GetAllAsync();
            var deliveryPersonDtos = deliveryPersons.Select(dp => new DeliveryPersonDto
            {
                Name = dp.Name,
                CNPJ = dp.CNPJ,
                BirthDate = dp.BirthDate,
                CNHNumber = dp.CNHNumber,
                CNHType = dp.CNHType,
                CNHImage = dp.CNHImage
            }).ToList();

            return Result<List<DeliveryPersonDto>>.Success(deliveryPersonDtos);
        }

        public async Task<Result> CheckCnpjAsync(string cnpj)
        {
            if (await _deliveryPersonRepository.ExistsByCNPJAsync(cnpj))
                return Result.Failure("CNPJ já cadastrado.");
            return Result.Success();
        }

        public async Task<Result> CheckCnhAsync(string cnh)
        {
            if (await _deliveryPersonRepository.ExistsByCNHAsync(cnh))
                return Result.Failure("Número da CNH já cadastrado.");
            return Result.Success();
        }

        public async Task<Result<Rent>> RentMotorcycleAsync(int deliveryPersonId, RentRequestDTO rentRequestDTO)
        {
            var deliveryPerson = await _deliveryPersonRepository.GetByIdAsync(deliveryPersonId);

            if (deliveryPerson == null)
                return Result<Rent>.Failure("Entregador não encontrado.");

            if (deliveryPerson.CNHType != "A")
                return Result<Rent>.Failure("Somente entregadores com categoria A podem alugar moto.");

            if (rentRequestDTO.StartDate <= DateTime.UtcNow)
                return Result<Rent>.Failure("A data de início deve ser posterior à data atual.");

            if (rentRequestDTO.EndDate <= rentRequestDTO.StartDate)
                return Result<Rent>.Failure("A data de término deve ser posterior à data de início.");

            var dailyRate = rentRequestDTO.Plan switch
            {
                RentPlan.SevenDays => 30.00,
                RentPlan.FifteenDays => 28.00,
                RentPlan.ThirtyDays => 22.00,
                RentPlan.FortyFiveDays => 20.00,
                RentPlan.FiftyDays => 18.00,
                _ => throw new ArgumentException("Plano de aluguel inválido.")
            };

            var totalCost = dailyRate * (rentRequestDTO.EndDate - rentRequestDTO.StartDate).Days;

            var rent = new Rent
            {
                RentDate = rentRequestDTO.StartDate,
                ReturnDate = rentRequestDTO.EndDate,
                TotalAmount = totalCost,
                CustomerId = rentRequestDTO.CustomerId,  
                VehicleId = rentRequestDTO.VehicleId, 
                Status = RentStatus.Pending,  
                CreatedAt = DateTime.UtcNow
            };

            await _rentRepository.AddAsync(rent);
            return Result<Rent>.Success(rent);
        }

        public async Task<Result<decimal>> CalculateReturnAmountAsync(int rentId, DateTime returnDate)
        {
            var rent = await _rentRepository.GetByIdAsync(rentId);

            if (rent == null)
                return Result<decimal>.Failure("Rent not found.");

            var totalAmount = rent.TotalAmount;
            var planDuration = (rent.ReturnDate - rent.RentDate).Days;
            var dailyRate = totalAmount / planDuration;

            if (returnDate < rent.ReturnDate)
            {
                var daysNotUsed = (rent.ReturnDate - returnDate).Days;
                decimal penaltyPercentage = planDuration switch
                {
                    7 => 0.20m, 
                    15 => 0.40m, 
                    _ => 0
                };

                var penaltyAmount = (decimal)(dailyRate * daysNotUsed) * penaltyPercentage;
                var totalReturnAmount = (decimal)totalAmount - penaltyAmount;
                return Result<decimal>.Success(totalReturnAmount);
            }
            else if (returnDate > rent.ReturnDate) 
            {
                var daysLate = (returnDate - rent.ReturnDate).Days;
                var lateFee = daysLate * 50; // R$50,00 por diária adicional
                var totalReturnAmount = totalAmount + lateFee;
                return Result<decimal>.Success((decimal)totalReturnAmount);
            }

            return Result<decimal>.Success((decimal)totalAmount);
        }
    }
}
