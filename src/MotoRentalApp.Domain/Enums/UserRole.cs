using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotoRentalApp.Domain.Enums
{
    public enum UserRole
    {
        Admin = 1,        // Usuário com permissões administrativas
        Entregador = 2    // Usuário com permissões de locação de motos
    }
}