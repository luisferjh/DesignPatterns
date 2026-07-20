using PaymentProcessor.Interfaces;
using PaymentProcessor.Models;

namespace PaymentProcessor;

public class PaymentProcessor : IPaymentProcessor
{
    public PaymentResult ProcessPayment(PaymentRequest request)
    {
        var transactionId = $"txn-{Guid.NewGuid():N}";

        return new PaymentResult
        {
            TransactionId = transactionId,
            Status = "Authorized",
            Timestamp = DateTime.UtcNow,
            MaskedCard = string.Empty
        };
    }
}
