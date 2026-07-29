using CheckoutOrder.Interfaces;
using CheckoutOrder.Models;

namespace CheckoutOrder.Services;

public class ShippingService : IShippingService
{
    public ShippingQuote CalculateShipping(ShippingAddress address, int totalItemCount)
    {
        var isDomestic = string.Equals(address.Country, "Colombia", StringComparison.OrdinalIgnoreCase);

        decimal baseCost = isDomestic ? 8000m : 45000m;
        int baseDays = isDomestic ? 3 : 10;

        // Cada 5 ítems adicionales suman un poco de costo y tiempo,
        // simulando el impacto de peso/volumen.
        var extraUnits = totalItemCount / 5;
        var cost = baseCost + (extraUnits * 1500m);
        var days = baseDays + extraUnits;

        var carrier = isDomestic ? "Servientrega" : "DHL Internacional";

        return new ShippingQuote
        {
            Cost = cost,
            EstimatedDays = days,
            Carrier = carrier
        };
    }
}