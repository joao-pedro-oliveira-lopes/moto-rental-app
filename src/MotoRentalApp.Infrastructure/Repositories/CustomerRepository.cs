using MotoRentalApp.Application.Interfaces.Repositories;
using MotoRentalApp.Domain.Entities;
using MotoRentalApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MotoRentalApp.Infrastructure.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Customer>> GetCustomersByCityAsync(string city)
        {
            return await _context.Customers.Where(c => c.City == city).ToListAsync();
        }
    }
}



