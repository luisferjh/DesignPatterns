using ECommerceDecorator.Models;

namespace ECommerceDecorator.Interfaces
{
    public interface IOrderProcessor
    {
        Task<Order> ProcessAsync(Order order);
    }  
};

