using MotoRentalApp.Domain.Entities;

namespace MotoRentalApp.Application.Interfaces.Repositories
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Task<List<Customer>> GetCustomersByCityAsync(string city);
    }
}
