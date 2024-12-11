using MotoRentalApp.Application.Interfaces.Repositories;
using MotoRentalApp.Domain.Entities;
using MotoRentalApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using MotoRentalApp.Domain.Enums;

namespace MotoRentalApp.Infrastructure.Repositories
{
    public class VehicleRepository : BaseRepository<Vehicle>, IVehicleRepository
    {
        private readonly AppDbContext _context;

        public VehicleRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Vehicle>> GetAllVehiclesAsync()
        {
            return await _context.Vehicles.ToListAsync();
        }

        // Método para buscar veículo por placa
        public async Task<Vehicle> GetByLicensePlateAsync(string licensePlate)
        {
            return await _context.Vehicles
                .FirstOrDefaultAsync(v => v.Plate == licensePlate);
        }

        // Método para verificar se um veículo com a placa já existe
        public async Task<bool> ExistsByLicensePlateAsync(string licensePlate)
        {
            return await _context.Vehicles
                .AnyAsync(v => v.Plate == licensePlate);
        }

        // Método para verificar se o veículo tem locações ativas
        public async Task<bool> HasActiveRentsAsync(int vehicleId)
        {
            return await _context.Rents
                .AnyAsync(r => r.VehicleId == vehicleId && r.Status == RentStatus.Active);
        }

        // Método para buscar todos os veículos
        public ICollection<Vehicle> GetAllVehicles()
        {
            return _context.Vehicles.ToList();
        }

        // Método para buscar veículos disponíveis em um intervalo de datas
        public async Task<List<Vehicle>> GetAvailableVehiclesAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Vehicles
                .Where(v => !v.Rents.Any(r => r.RentDate < endDate && r.ReturnDate > startDate))
                .ToListAsync();
        }
    }
}
