using MotoRentalApp.Domain.Entities;

namespace MotoRentalApp.Application.Interfaces.Repositories
{
    public interface IVehicleRepository : IBaseRepository<Vehicle>
    {
        Task<Vehicle> GetByLicensePlateAsync(string licensePlate);
        Task<bool> ExistsByLicensePlateAsync(string licensePlate);
        Task<bool> HasActiveRentsAsync(int vehicleId);
        Task<List<Vehicle>> GetAvailableVehiclesAsync(DateTime startDate, DateTime endDate);
        Task<List<Vehicle>> GetAllVehiclesAsync();
    }
}


