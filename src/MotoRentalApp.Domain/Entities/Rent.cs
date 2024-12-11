using MotoRentalApp.Domain.Enums;

namespace MotoRentalApp.Domain.Entities;

public class Rent
{
    public int Id { get; set; }
    public DateTime RentDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public double TotalAmount { get; set; }
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }
    public int VehicleId { get; set; }
    public Vehicle Vehicle { get; set; }
    public ICollection<Fine> Fines { get; set; }
    public RentStatus Status { get; set; }
}
