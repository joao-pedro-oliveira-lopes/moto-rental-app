using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotoRentalApp.Application.Models
{
    public class RentDto
    {
        public int Id { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public double TotalAmount { get; set; }
        public int CustomerId { get; set; }
        public int VehicleId { get; set; }
    }
}