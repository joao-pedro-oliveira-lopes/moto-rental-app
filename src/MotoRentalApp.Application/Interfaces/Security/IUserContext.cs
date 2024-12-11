using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotoRentalApp.Application.Interfaces.Security
{
    public interface IUserContext
    {
        string GetCurrentUserId();
        string GetCurrentUserRole();
    }
}