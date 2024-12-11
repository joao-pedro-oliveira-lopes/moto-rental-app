using Microsoft.AspNetCore.Http;

namespace MotoRentalApp.Application.Interfaces.Services
{
    public interface IStorageService
    {
        Task<string> UploadCnhImageAsync(IFormFile file);
    }
}