using PaymentProcessor.Interfaces;

namespace PaymentProcessor.Decorators;

public abstract class PaymentProcessorDecorator : IPaymentProcessor
{
    protected readonly IPaymentProcessor _inner;

    protected PaymentProcessorDecorator(IPaymentProcessor inner)
    {
        _inner = inner;
    }

    public abstract Models.PaymentResult ProcessPayment(Models.PaymentRequest request);
}
