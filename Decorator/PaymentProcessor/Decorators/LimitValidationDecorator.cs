using PaymentProcessor.Interfaces;
using PaymentProcessor.Models;
using PaymentProcessor.Services;

namespace PaymentProcessor.Decorators;

public class LimitValidationDecorator : PaymentProcessorDecorator
{
    private readonly MerchantConfig _config;
    private readonly DailyLimitTracker _tracker;

    public LimitValidationDecorator(IPaymentProcessor inner, MerchantConfig config, DailyLimitTracker tracker) : base(inner)
    {
        _config = config;
        _tracker = tracker;
    }

    public override PaymentResult ProcessPayment(PaymentRequest request)
    {
        if (request.MerchantId.Equals(_config.MerchantId, StringComparison.OrdinalIgnoreCase))
        {
            var currentTotal = _tracker.GetTotal(request.MerchantId) + request.Amount;
            if (currentTotal > _config.DailyLimit)
            {
                return new PaymentResult
                {
                    TransactionId = "Daily limit exceeded",
                    Status = "Rejected",
                    Timestamp = DateTime.UtcNow,
                    MaskedCard = string.Empty
                };
            }

            _tracker.AddCharge(request.MerchantId, request.Amount);
        }

        return _inner.ProcessPayment(request);
    }
}
