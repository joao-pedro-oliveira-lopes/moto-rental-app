using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotoRentalApp.Application.Models
{
    public class RentRequestDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public RentPlan Plan { get; set; }
        public int CustomerId { get; set; } 
        public int VehicleId { get; set; }
    }

    public enum RentPlan
    {
        SevenDays,
        FifteenDays,
        ThirtyDays,
        FortyFiveDays,
        FiftyDays
    }
    
}