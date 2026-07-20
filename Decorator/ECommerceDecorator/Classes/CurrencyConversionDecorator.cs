using ECommerceDecorator.Interfaces;
using ECommerceDecorator.Models;


namespace ECommerceDecorator.Classes;

public class CurrencyConversionDecorator : OrderProcessorDecorator
{
    private readonly IExchangeRateService _exchangeRateService;
    private const string LocalCurrency = "COP";

    public CurrencyConversionDecorator(IOrderProcessor inner, IExchangeRateService exchangeRateService)
        : base(inner)
    {
        _exchangeRateService = exchangeRateService;
    }

    public override async Task<Order> ProcessAsync(Order order)
    {
        order = await _inner.ProcessAsync(order);

        var targetCurrency = await _exchangeRateService
            .GetCurrencyForCountryAsync(order.DestinationCountry);

        var rate = await _exchangeRateService
            .GetRateAsync(LocalCurrency, targetCurrency);

        var totalBeforeConversion = order.Total;
        order.Total = Math.Round(order.Total * rate, 2);
        order.Currency = targetCurrency;

        order.ProcessingLog.Add(
            $"[Moneda] {totalBeforeConversion:C} COP → {order.Total} {targetCurrency} (tasa: {rate})");

        return order;
    }
}