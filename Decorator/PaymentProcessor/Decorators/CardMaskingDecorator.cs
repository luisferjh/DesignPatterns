using PaymentProcessor.Interfaces;
using PaymentProcessor.Models;

namespace PaymentProcessor.Decorators;

public class CardMaskingDecorator : PaymentProcessorDecorator
{
    public CardMaskingDecorator(IPaymentProcessor inner) : base(inner)
    {
    }

    public override PaymentResult ProcessPayment(PaymentRequest request)
    {
        var result = _inner.ProcessPayment(request);
        result.MaskedCard = MaskCard(request.CardToken);
        return result;
    }

    private static string MaskCard(string cardToken)
    {
        if (string.IsNullOrWhiteSpace(cardToken) || cardToken.Length < 4)
        {
            return string.Empty;
        }

        var lastFour = cardToken[^4..];
        return $"****-****-****-{lastFour}";
    }
}
