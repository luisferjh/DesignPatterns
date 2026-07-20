namespace ECommerceDecorator.Interfaces;

public interface IStockService
{
    Task<bool> HasStockAsync(string productId, int quantity);
}

public interface IExchangeRateService
{
    Task<decimal> GetRateAsync(string fromCurrency, string toCurrency);
    Task<string> GetCurrencyForCountryAsync(string countryCode);
}

public interface IEmailService
{
    Task SendAsync(string to, string subject, string body);
}

public interface IAuditService
{
    Task LogAsync(string orderId, string stage, object snapshot);
}