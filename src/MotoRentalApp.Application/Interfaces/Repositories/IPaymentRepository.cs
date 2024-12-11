using MotoRentalApp.Domain.Entities;

namespace MotoRentalApp.Application.Interfaces.Repositories
{
    public interface IPaymentRepository : IBaseRepository<Payment>
    {
        Task<List<Payment>> GetPaymentsByRentAsync(int rentId);
    }
}
