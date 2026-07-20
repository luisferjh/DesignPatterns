using ECommerceDecorator.Interfaces;
using ECommerceDecorator.Models;

namespace ECommerceDecorator.Classes
{
    public class RawOrderProcessor : IOrderProcessor
    {
        public Task<Order> ProcessAsync(Order order)
        {
            order.Subtotal = order.Items.Sum(i => i.UnitPrice * i.Quantity);
            order.Total = order.Subtotal;
            order.ProcessingLog.Add($"[Base] Subtotal calculado: {order.Total:C}");

            return Task.FromResult(order);
        }
    }
}