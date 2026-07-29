using CheckoutOrder.Interfaces;
using CheckoutOrder.Models;

namespace CheckoutOrder.Services;
public class PricingService : IPricingService
    {
        // Cupones válidos: código -> porcentaje de descuento (0.10 = 10%).
        private readonly Dictionary<string, decimal> _validCoupons = new()
        {
            { "WELCOME10", 0.10m },
            { "VIP20", 0.20m }
        };
 
        public PricingResult CalculateTotal(List<OrderItem> items, string? couponCode)
        {
            var subtotal = items.Sum(i => i.UnitPrice * i.Quantity);
 
            // Descuento por volumen: si algún ítem individual pide 10+ unidades,
            // ese ítem obtiene un 5% de descuento sobre su propio subtotal.
            decimal volumeDiscount = 0;
            foreach (var item in items)
            {
                if (item.Quantity >= 10)
                {
                    volumeDiscount += (item.UnitPrice * item.Quantity) * 0.05m;
                }
            }
 
            // Descuento por cupón, aplicado sobre el subtotal ya con descuento por volumen.
            decimal couponDiscount = 0;
            var afterVolumeDiscount = subtotal - volumeDiscount;
 
            if (!string.IsNullOrWhiteSpace(couponCode) &&
                _validCoupons.TryGetValue(couponCode.ToUpperInvariant(), out var percentage))
            {
                couponDiscount = afterVolumeDiscount * percentage;
            }
 
            var totalDiscount = volumeDiscount + couponDiscount;
            var total = subtotal - totalDiscount;
 
            return new PricingResult
            {
                Subtotal = subtotal,
                DiscountApplied = totalDiscount,
                Total = total
            };
        }
    }