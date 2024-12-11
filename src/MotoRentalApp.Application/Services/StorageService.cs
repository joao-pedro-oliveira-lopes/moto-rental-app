using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MotoRentalApp.Application.Interfaces.Services;

namespace MotoRentalApp.Application.Services
{
    public class StorageService : IStorageService
    {
        private readonly string _storagePath = Path.Combine(Directory.GetCurrentDirectory(), "uploads", "cnh");

        public StorageService()
        {
            if (!Directory.Exists(_storagePath))
            {
                Directory.CreateDirectory(_storagePath);
            }
        }

        public async Task<string> UploadCnhImageAsync(IFormFile file)
        {
            if (file.Length == 0)
                throw new ArgumentException("File is empty.");

            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            if (fileExtension != ".png" && fileExtension != ".bmp")
                throw new ArgumentException("Invalid file format. Only PNG and BMP are allowed.");

            var fileName = $"{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(_storagePath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return filePath;
        }
    }
}