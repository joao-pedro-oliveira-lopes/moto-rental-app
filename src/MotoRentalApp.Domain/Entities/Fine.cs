namespace MotoRentalApp.Domain.Entities;

public class Fine
{
    public int Id { get; set; }
    public string Description { get; set; }
    public double Amount { get; set; }
    public DateTime IssuedDate { get; set; }
    
    // Chave estrangeira para Rent (Locação)
    public int RentId { get; set; }
    public Rent Rent { get; set; }
}