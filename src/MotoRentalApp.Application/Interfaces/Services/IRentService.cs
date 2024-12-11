using MotoRentalApp.Application.Common;
using MotoRentalApp.Application.Models;

namespace MotoRentalApp.Application.Interfaces.Services
{
public interface IRentService
    {
        Task<Result<RentDto>> CreateRentAsync(RentDto rentDto);
        Task<Result<RentDto>> GetRentByIdAsync(int id);
        Task<Result<IEnumerable<RentDto>>> GetAllRentsAsync();
    }
}