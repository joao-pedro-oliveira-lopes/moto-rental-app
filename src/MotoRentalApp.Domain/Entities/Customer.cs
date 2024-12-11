namespace MotoRentalApp.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string City { get; set; }  
        public ICollection<Rent> Rents { get; set; }
    }
}