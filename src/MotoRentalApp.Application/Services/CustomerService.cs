using MotoRentalApp.Application.Common;
using MotoRentalApp.Application.Interfaces.Repositories;
using MotoRentalApp.Application.Interfaces.Services;
using MotoRentalApp.Application.Models;
using MotoRentalApp.Domain.Entities;

namespace MotoRentalApp.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Result<Customer>> CreateCustomerAsync(CustomerDto customerDto)
        {
            var customer = new Customer
            {
                Name = customerDto.Name,
                CPF = customerDto.CPF,
                Email = customerDto.Email,
                Phone = customerDto.Phone,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _customerRepository.AddAsync(customer);

            return Result<Customer>.Success(customer);
        }

        public async Task<Result<Customer>> GetCustomerByIdAsync(int id)
        {
            var customer = await _customerRepository.GetByIdAsync(id);
            if (customer == null)
            {
                return Result<Customer>.Failure("Customer not found");
            }

            return Result<Customer>.Success(customer);
        }

        public async Task<Result<IEnumerable<CustomerDto>>> GetAllCustomersAsync()
        {
            var customers = await _customerRepository.GetAllAsync();
            var customerDtos = customers.Select(c => new CustomerDto
            {
                Name = c.Name,
                CPF = c.CPF,
                Email = c.Email,
                Phone = c.Phone
            });

            return Result<IEnumerable<CustomerDto>>.Success(customerDtos);
        }
    }
}
