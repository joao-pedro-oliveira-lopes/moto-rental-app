using MotoRentalApp.Application.Common;
using MotoRentalApp.Application.Interfaces.Repositories;
using MotoRentalApp.Application.Interfaces.Services;
using MotoRentalApp.Application.Models;
using MotoRentalApp.Domain.Entities;
using MotoRentalApp.Domain.Enums;

namespace MotoRentalApp.Application.Services
{
    public class RentService : IRentService
    {
        private readonly IRentRepository _rentRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IVehicleRepository _vehicleRepository;

        public RentService(IRentRepository rentRepository, ICustomerRepository customerRepository, IVehicleRepository vehicleRepository)
        {
            _rentRepository = rentRepository;
            _customerRepository = customerRepository;
            _vehicleRepository = vehicleRepository;
        }

        public async Task<Result<RentDto>> CreateRentAsync(RentDto rentDto)
        {
            var customer = await _customerRepository.GetByIdAsync(rentDto.CustomerId);
            if (customer == null)
            {
                return Result<RentDto>.Failure("Customer not found");
            }

            var vehicle = await _vehicleRepository.GetByIdAsync(rentDto.VehicleId);
            if (vehicle == null)
            {
                return Result<RentDto>.Failure("Vehicle not found");
            }

            var rent = new Rent
            {
                RentDate = rentDto.RentDate,
                ReturnDate = rentDto.ReturnDate,
                TotalAmount = rentDto.TotalAmount,
                CustomerId = rentDto.CustomerId,
                VehicleId = rentDto.VehicleId,
                Status = RentStatus.Pending,  
                CreatedAt = DateTime.UtcNow
            };

            await _rentRepository.AddAsync(rent);

            return Result<RentDto>.Success(rentDto);
        }

        public async Task<Result<RentDto>> GetRentByIdAsync(int id)
        {
            var rent = await _rentRepository.GetByIdAsync(id);
            if (rent == null)
            {
                return Result<RentDto>.Failure("Rent not found");
            }

            var rentDto = new RentDto
            {
                CustomerId = rent.CustomerId,
                VehicleId = rent.VehicleId,
                RentDate = rent.RentDate,
                ReturnDate = rent.ReturnDate,
                TotalAmount = rent.TotalAmount,
            };

            return Result<RentDto>.Success(rentDto);
        }

        public async Task<Result<IEnumerable<RentDto>>> GetAllRentsAsync()
        {
            var rents = await _rentRepository.GetAllAsync();
            var rentDtos = rents.Select(r => new RentDto
            {
                CustomerId = r.CustomerId,
                VehicleId = r.VehicleId,
                RentDate = r.RentDate,
                ReturnDate = r.ReturnDate,
                TotalAmount = r.TotalAmount
            });

            return Result<IEnumerable<RentDto>>.Success(rentDtos);
        }
    }
}