using ECommerceDecorator.Interfaces;
using ECommerceDecorator.Models;

namespace ECommerceDecorator.Classes;

public class StockValidationDecorator : OrderProcessorDecorator
{
    private readonly IStockService _stockService;

    public StockValidationDecorator(IOrderProcessor inner, IStockService stockService)
        : base(inner)
    {
        _stockService = stockService;
    }

    public override async Task<Order> ProcessAsync(Order order)
    {
        foreach (var item in order.Items)
        {
            var hasStock = await _stockService.HasStockAsync(item.ProductId, item.Quantity);
            if (!hasStock)
                throw new InvalidOperationException(
                    $"Stock insuficiente para el producto '{item.ProductName}' (id: {item.ProductId}).");
        }

        order.ProcessingLog.Add("[Stock] Todos los productos tienen stock disponible.");

        return await _inner.ProcessAsync(order);
    }
}