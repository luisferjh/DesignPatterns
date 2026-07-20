using ECommerceDecorator.Models;

namespace ECommerceDecorator.Interfaces
{
    public abstract class OrderProcessorDecorator : IOrderProcessor
    {
        protected readonly IOrderProcessor _inner;
 
        protected OrderProcessorDecorator(IOrderProcessor inner)
        {
            _inner = inner;
        }
 
        public virtual Task<Order> ProcessAsync(Order order)
            => _inner.ProcessAsync(order);
        }
}