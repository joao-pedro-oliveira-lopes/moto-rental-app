using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MotoRentalApp.Domain.Entities;

namespace MotoRentalApp.Application.Interfaces.Repositories
{
    public interface IDeliveryPersonRepository : IBaseRepository<DeliveryPerson>
    {
        Task<bool> ExistsByCNPJAsync(string cnpj);
        Task<bool> ExistsByCNHAsync(string cnhNumber);
    }
}