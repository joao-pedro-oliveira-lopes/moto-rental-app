using Microsoft.Extensions.Logging;
using MotoRentalApp.Application.Events;
using MotoRentalApp.Application.Interfaces.Messaging;
using MotoRentalApp.Application.Interfaces.Repositories;
using MotoRentalApp.Domain.Entities;


namespace MotoRentalApp.Infrastructure.Messaging
{
    public class Vehicle2024EventConsumer : IEventConsumer<Vehicle2024Event>
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly ILogger<Vehicle2024EventConsumer> _logger;

        public Vehicle2024EventConsumer(IVehicleRepository vehicleRepository, ILogger<Vehicle2024EventConsumer> logger)
        {
            _vehicleRepository = vehicleRepository;
            _logger = logger;
        }

        public async Task ConsumeAsync(Vehicle2024Event eventMessage)
        {
            try
            {
                var vehicle = new Vehicle
                {
                    Id = eventMessage.Vehicle.Id,
                    Model = eventMessage.Vehicle.Model,
                    Year = eventMessage.Vehicle.Year,
                    Plate = eventMessage.Vehicle.Plate
                };

                await _vehicleRepository.AddAsync(vehicle);
                _logger.LogInformation($"Veículo {vehicle.Plate} de 2024 armazenado com sucesso.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro ao processar o evento de veículo 2024: {ex.Message}");
            }
        }
    }
}