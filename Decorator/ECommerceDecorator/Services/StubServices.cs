using ECommerceDecorator.Interfaces;
using ECommerceDecorator.Models;

namespace  ECommerceDecorator.Services;

// ── Stub: Stock ────────────────────────────────────────────────────
public class StubStockService : IStockService
{
    // Simula que todos los productos tienen stock,
    // excepto el id "OUT_OF_STOCK" para poder probar el error.
    public Task<bool> HasStockAsync(string productId, int quantity)
    {
        var hasStock = productId != "OUT_OF_STOCK";
        return Task.FromResult(hasStock);
    }
}

// ── Stub: Exchange Rate ────────────────────────────────────────────
public class StubExchangeRateService : IExchangeRateService
{
    private static readonly Dictionary<string, string> CountryCurrencies = new()
    {
        { "US", "USD" },
        { "MX", "MXN" },
        { "BR", "BRL" },
        { "CO", "COP" },
        { "EU", "EUR" },
    };

    private static readonly Dictionary<string, decimal> Rates = new()
    {
        { "COP_USD", 0.00025m },
        { "COP_MXN", 0.0043m  },
        { "COP_BRL", 0.0013m  },
        { "COP_EUR", 0.00023m },
    };

    public Task<string> GetCurrencyForCountryAsync(string countryCode)
    {
        var currency = CountryCurrencies.GetValueOrDefault(countryCode.ToUpper(), "USD");
        return Task.FromResult(currency);
    }

    public Task<decimal> GetRateAsync(string fromCurrency, string toCurrency)
    {
        var key = $"{fromCurrency}_{toCurrency}";
        var rate = Rates.GetValueOrDefault(key, 1m);
        return Task.FromResult(rate);
    }
}

// ── Stub: Email ────────────────────────────────────────────────────
public class StubEmailService : IEmailService
{
    public Task SendAsync(string to, string subject, string body)
    {
        Console.WriteLine($"[EMAIL] To: {to} | Subject: {subject}");
        return Task.CompletedTask;
    }
}

// ── Stub: Audit ────────────────────────────────────────────────────
public class StubAuditService : IAuditService
{
    public Task LogAsync(string orderId, string stage, object snapshot)
    {
        Console.WriteLine($"[AUDIT] Order: {orderId} | Stage: {stage}");
        return Task.CompletedTask;
    }
}