using MotoRentalApp.Application.Common;
using MotoRentalApp.Application.Models;
using MotoRentalApp.Domain.Entities;

namespace MotoRentalApp.Application.Interfaces.Services
{
    public interface ICustomerService
    {
        Task<Result<Customer>> GetCustomerByIdAsync(int id);
        Task<Result<IEnumerable<CustomerDto>>> GetAllCustomersAsync();
        Task<Result<Customer>> CreateCustomerAsync(CustomerDto customerDto);
    }
}