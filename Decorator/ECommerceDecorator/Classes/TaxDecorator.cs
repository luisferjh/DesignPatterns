

using ECommerceDecorator.Interfaces;
using ECommerceDecorator.Models;

namespace ECommerceDecorator.Classes;

public class TaxDecorator : OrderProcessorDecorator
{
    private readonly decimal _taxPercentage;
    private readonly string _taxLabel;

    // La tasa y la etiqueta se inyectan desde fuera:
    // nacional → IVA 19%, internacional → arancel 5%, etc.
    public TaxDecorator(IOrderProcessor inner, decimal taxPercentage, string taxLabel)
        : base(inner)
    {
        _taxPercentage = taxPercentage;
        _taxLabel = taxLabel;
    }

    public override async Task<Order> ProcessAsync(Order order)
    {
        order = await _inner.ProcessAsync(order);

        order.Tax = Math.Round(order.Total * (_taxPercentage / 100), 2);
        order.Total += order.Tax;

        order.ProcessingLog.Add(
            $"[Impuesto] {_taxLabel} {_taxPercentage}%: +{order.Tax:C} → Total: {order.Total:C}");

        return order;
    }
}