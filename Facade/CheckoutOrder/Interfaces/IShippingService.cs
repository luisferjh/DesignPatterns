using CheckoutOrder.Models;

namespace CheckoutOrder.Interfaces;

public interface IShippingService
{
    /// <summary>
    /// Calcula costo y tiempo estimado de envío según la dirección
    /// de destino y el peso/volumen implícito del pedido (aquí simplificado
    /// por cantidad de ítems).
    /// </summary>
    ShippingQuote CalculateShipping(ShippingAddress address, int totalItemCount);
}