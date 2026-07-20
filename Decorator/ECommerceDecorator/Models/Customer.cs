namespace ECommerceDecorator.Models
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = default!;
        public string Email { get; set; } = default!;
        public CustomerType Type { get; set; }
    }
}
    
