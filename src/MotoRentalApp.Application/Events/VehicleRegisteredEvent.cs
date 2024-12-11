using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MotoRentalApp.Domain.Entities;

namespace MotoRentalApp.Application.Events
{
    public class VehicleRegisteredEvent
    {
        public Vehicle Vehicle { get; }

        public VehicleRegisteredEvent(Vehicle vehicle)
        {
            Vehicle = vehicle;
        }
    }

    // Evento específico para o ano de 2024
    public class Vehicle2024Event
    {
        public Vehicle Vehicle { get; }

        public Vehicle2024Event(Vehicle vehicle)
        {
            Vehicle = vehicle;
        }
    }
}