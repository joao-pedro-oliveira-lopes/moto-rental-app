using Microsoft.EntityFrameworkCore;
using MotoRentalApp.Application.Interfaces.Repositories;
using MotoRentalApp.Domain.Entities;
using MotoRentalApp.Infrastructure.Data;

namespace MotoRentalApp.Infrastructure.Repositories
{
    public class DeliveryPersonRepository : BaseRepository<DeliveryPerson>, IDeliveryPersonRepository
    {
        private readonly AppDbContext _context;

        public DeliveryPersonRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> ExistsByCNPJAsync(string cnpj)
        {
            return await _context.DeliveryPersons
                                 .AnyAsync(dp => dp.CNPJ == cnpj);
        }

        public async Task<bool> ExistsByCNHAsync(string cnhNumber)
        {
            return await _context.DeliveryPersons
                                 .AnyAsync(dp => dp.CNHNumber == cnhNumber);
        }

        public async Task AddAsync(DeliveryPerson deliveryPerson)
        {
            await _context.DeliveryPersons.AddAsync(deliveryPerson);
            await _context.SaveChangesAsync();
        }
    }
}