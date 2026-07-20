using ECommerceDecorator.Interfaces;
using ECommerceDecorator.Models;

namespace ECommerceDecorator.Classes
{
    public class DiscountDecorator : OrderProcessorDecorator
    {
        private readonly decimal _discountPercentage;
        
        public DiscountDecorator(IOrderProcessor inner, decimal discountPercentage)
        : base(inner)
        {
            _discountPercentage = discountPercentage;
        }

        public override async Task<Order> ProcessAsync(Order order)
        {
    
            order = await _inner.ProcessAsync(order);
 
            order.Discount = Math.Round(order.Total * (_discountPercentage / 100), 2);
            order.Total -= order.Discount;
            order.ProcessingLog.Add($"[Descuento] {_discountPercentage}% aplicado: -{order.Discount:C} → Total: {order.Total:C}");
    
            return order;
        }       
    }
}