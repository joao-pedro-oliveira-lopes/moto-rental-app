using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotoRentalApp.Domain.Entities
{
    public class DeliveryPerson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public DateTime BirthDate { get; set; }
        public string CNHNumber { get; set; }
        public string CNHType { get; set; } // "A", "B" ou "A+B"
        public string CNHImage { get; set; } // Caminho ou URL da imagem da CNH
    }
}