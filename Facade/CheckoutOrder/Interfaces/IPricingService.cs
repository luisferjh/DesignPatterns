using CheckoutOrder.Models;

namespace CheckoutOrder.Interfaces;

public interface IPricingService
{
    /// <summary>
    /// Calcula el total del pedido aplicando descuento por cantidad
    /// y, opcionalmente, un cupón.
    /// </summary>
    PricingResult CalculateTotal(List<OrderItem> items, string? couponCode);
}