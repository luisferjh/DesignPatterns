using Microsoft.Extensions.Logging;
using PaymentProcessor.Interfaces;
using PaymentProcessor.Models;

namespace PaymentProcessor.Decorators;

public class LoggingDecorator : PaymentProcessorDecorator
{
    private readonly ILogger<LoggingDecorator> _logger;

    public LoggingDecorator(IPaymentProcessor inner, ILogger<LoggingDecorator> logger) : base(inner)
    {
        _logger = logger;
    }

    public override PaymentResult ProcessPayment(PaymentRequest request)
    {
        _logger.LogInformation("Processing payment for merchant {MerchantId}", request.MerchantId);
        var result = _inner.ProcessPayment(request);
        _logger.LogInformation("Payment completed with status {Status}", result.Status);
        return result;
    }
}
