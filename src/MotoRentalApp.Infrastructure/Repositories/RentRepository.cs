using MotoRentalApp.Application.Interfaces.Repositories;
using MotoRentalApp.Domain.Entities;
using MotoRentalApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MotoRentalApp.Infrastructure.Repositories
{
    public class RentRepository : BaseRepository<Rent>, IRentRepository
    {
        private readonly AppDbContext _context;

        public RentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Rent>> GetRentsByCustomerAsync(int customerId)
        {
            return await _context.Rents.Where(r => r.CustomerId == customerId).ToListAsync();
        }

        public async Task<List<Rent>> GetRentsByVehicleAsync(int vehicleId)
        {
            return await _context.Rents.Where(r => r.VehicleId == vehicleId).ToListAsync();
        }
    }
}



