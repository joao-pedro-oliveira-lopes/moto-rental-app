using MotoRentalApp.Application.Interfaces.Repositories;
using MotoRentalApp.Application.Interfaces.Services;
using MotoRentalApp.Application.Common;
using MotoRentalApp.Domain.Entities;
using MotoRentalApp.Application.Interfaces.Messaging;
using MotoRentalApp.Application.Interfaces.Security;
using MotoRentalApp.Application.Models;
using MotoRentalApp.Application.Events;

namespace MotoRentalApp.Application.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IEventPublisher _eventPublisher;
        private readonly IUserContext _userContext;

        public VehicleService(IVehicleRepository vehicleRepository, IEventPublisher eventPublisher, IUserContext userContext)
        {
            _vehicleRepository = vehicleRepository;
            _eventPublisher = eventPublisher;
            _userContext = userContext;
        }

        
        public async Task<Result> CreateVehicleAsync(VehicleDto vehicleDto)
        {
            if (await _vehicleRepository.ExistsByLicensePlateAsync(vehicleDto.LicensePlate))
                return Result.Failure("Placa já cadastrada.");

            var vehicle = new Vehicle
            {
                Model = vehicleDto.Model,
                Year = vehicleDto.Year,
                Plate = vehicleDto.LicensePlate
            };

            await _vehicleRepository.AddAsync(vehicle);

            
            await _eventPublisher.Publish(new VehicleRegisteredEvent(vehicle));

            
            if (vehicle.Year == 2024)
            {
                await _eventPublisher.Publish(new Vehicle2024Event(vehicle));
            }

            return Result.Success();
        }

        
        public async Task<Result<List<VehicleDto>>> GetVehiclesAsync(string plate = null)
        {
            var vehicles = await _vehicleRepository.GetAllVehiclesAsync();

            if (!string.IsNullOrEmpty(plate))
            {
                vehicles = vehicles.Where(v => v.Plate.Contains(plate)).ToList();
            }

            var vehicleDtos = vehicles.Select(v => new VehicleDto
            {
                Id = v.Id,
                Model = v.Model,
                Year = v.Year,
                LicensePlate = v.Plate
            }).ToList();

            return Result<List<VehicleDto>>.Success(vehicleDtos);
        }

        
        public async Task<Result> UpdateVehiclePlateAsync(int vehicleId, string newPlate)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(vehicleId);

            if (vehicle == null)
                return Result.Failure("Veículo não encontrado.");

            
            if (await _vehicleRepository.ExistsByLicensePlateAsync(newPlate))
                return Result.Failure("Já existe um veículo com essa placa.");

            vehicle.Plate = newPlate;
            await _vehicleRepository.UpdateAsync(vehicle);

            return Result.Success();
        }

        
        public async Task<Result> DeleteVehicleAsync(int vehicleId)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(vehicleId);

            if (vehicle == null)
                return Result.Failure("Veículo não encontrado.");

            
            if (await _vehicleRepository.HasActiveRentsAsync(vehicleId))
                return Result.Failure("Não é possível excluir o veículo, pois ele possui locações ativas.");

            await _vehicleRepository.DeleteAsync(vehicleId);
            return Result.Success();
        }

        
        public async Task<Result<List<VehicleDto>>> GetAvailableVehiclesAsync(DateTime startDate, DateTime endDate)
        {
            var availableVehicles = await _vehicleRepository.GetAvailableVehiclesAsync(startDate, endDate);

            var vehicleDtos = availableVehicles.Select(v => new VehicleDto
            {
                Id = v.Id,
                Model = v.Model,
                Year = v.Year,
                LicensePlate = v.Plate
            }).ToList();

            return Result<List<VehicleDto>>.Success(vehicleDtos);
        }
    }
}
