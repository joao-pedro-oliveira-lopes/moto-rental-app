namespace MotoRentalApp.Domain.Entities;

public class Payment
{
    public int Id { get; set; }
        public double Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        
        // Chave estrangeira para Rent (Locação)
        public int RentId { get; set; }
        public Rent Rent { get; set; }
}