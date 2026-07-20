namespace ECommerceDecorator.Models
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Customer Customer { get; set; } = default!;
        public List<OrderItem> Items { get; set; } = new();
        public string DestinationCountry { get; set; } = default!;
    
        // Campos que el pipeline va enriqueciendo
        public decimal Subtotal { get; set; }
        public decimal Discount { get; set; }
        public decimal Tax { get; set; }
        public decimal Total { get; set; }
        public string Currency { get; set; } = "COP";
        public List<string> ProcessingLog { get; set; } = new();
    }
}