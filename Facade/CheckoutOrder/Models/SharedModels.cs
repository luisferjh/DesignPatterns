namespace CheckoutOrder.Models
{
    /// <summary>
    /// Un ítem individual dentro del pedido.
    /// </summary>
    public class OrderItem
    {
        public string ProductId { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
 
    /// <summary>
    /// Dirección de envío simplificada.
    /// </summary>
    public class ShippingAddress
    {
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
    }
 
    /// <summary>
    /// Resultado de una reserva de inventario. Guarda el ReservationId
    /// para poder liberar la reserva más adelante si algo falla.
    /// </summary>
    public class ReservationResult
    {
        public bool Success { get; set; }
        public string? ReservationId { get; set; }
        public string? FailureReason { get; set; }
    }
 
    /// <summary>
    /// Resultado del cálculo de precio.
    /// </summary>
    public class PricingResult
    {
        public decimal Subtotal { get; set; }
        public decimal DiscountApplied { get; set; }
        public decimal Total { get; set; }
    }
 
    /// <summary>
    /// Resultado de un intento de cobro.
    /// </summary>
    public class PaymentResult
    {
        public bool Success { get; set; }
        public string? TransactionId { get; set; }
        public string? FailureReason { get; set; }
    }
 
    /// <summary>
    /// Resultado del cálculo de envío.
    /// </summary>
    public class ShippingQuote
    {
        public decimal Cost { get; set; }
        public int EstimatedDays { get; set; }
        public string Carrier { get; set; } = string.Empty;
    }

    /// <summary>
    /// DTO de entrada del endpoint POST /api/checkout.
    /// Es lo único que el controlador conoce; nunca ve los modelos
    /// internos de cada subsistema.
    /// </summary>
    public class CheckoutRequest
    {
        public string CustomerId { get; set; } = string.Empty;
        public List<OrderItem> Items { get; set; } = new();
        public string? CouponCode { get; set; }
        public ShippingAddress ShippingAddress { get; set; } = new();
    }

    /// <summary>
    /// DTO de salida unificado del checkout. Agrupa en un solo objeto
    /// el resultado de los 5 subsistemas, para que el consumidor de la API
    /// no tenga que interpretar respuestas separadas.
    /// </summary>
    public class CheckoutResult
    {
        public bool Success { get; set; }
        public string? FailureReason { get; set; }
 
        // Se llenan solo si Success = true.
        public string? TransactionId { get; set; }
        public decimal? TotalCharged { get; set; }
        public decimal? DiscountApplied { get; set; }
        public string? ShippingCarrier { get; set; }
        public int? EstimatedDeliveryDays { get; set; }
    }
}