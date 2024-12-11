using Microsoft.AspNetCore.Http;

namespace MotoRentalApp.Application.Models
{
    public class UpdateCnhImageDto
    {
        public IFormFile CnhImage { get; set; }
    }
}