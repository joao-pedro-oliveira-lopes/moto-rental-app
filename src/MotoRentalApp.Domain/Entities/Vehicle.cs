namespace MotoRentalApp.Domain.Entities;

public class Vehicle
{
    public int Id { get; set; }
    public string Plate { get; set; }
    public string Model { get; set; }
    public string Brand { get; set; }
    public int Year { get; set; }
    public string LicensePlate { get; set; }
    public double PricePerDay { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Relacionamento com a Locação (Rent)
    public ICollection<Rent> Rents { get; set; }
}