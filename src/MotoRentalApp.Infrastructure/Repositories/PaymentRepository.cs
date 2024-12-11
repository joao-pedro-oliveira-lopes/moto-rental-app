using MotoRentalApp.Application.Interfaces.Repositories;
using MotoRentalApp.Domain.Entities;
using MotoRentalApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MotoRentalApp.Infrastructure.Repositories
{
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        private readonly AppDbContext _context;

        public PaymentRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Payment>> GetPaymentsByRentAsync(int rentId)
        {
            return await _context.Payments.Where(p => p.RentId == rentId).ToListAsync();
        }
    }
}
