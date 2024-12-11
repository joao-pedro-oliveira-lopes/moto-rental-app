using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MotoRentalApp.Application.Common;
using MotoRentalApp.Application.Models;

namespace MotoRentalApp.Application.Interfaces.Services
{
    public interface IVehicleService
    {
        Task<Result> CreateVehicleAsync(VehicleDto vehicleDto);
        Task<Result<List<VehicleDto>>> GetVehiclesAsync(string plate = null);
        Task<Result> UpdateVehiclePlateAsync(int vehicleId, string newPlate);
        Task<Result> DeleteVehicleAsync(int vehicleId);
        Task<Result<List<VehicleDto>>> GetAvailableVehiclesAsync(DateTime startDate, DateTime endDate);
    }

}