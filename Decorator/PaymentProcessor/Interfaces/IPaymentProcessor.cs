namespace PaymentProcessor.Interfaces;

public interface IPaymentProcessor
{
    Models.PaymentResult ProcessPayment(Models.PaymentRequest request);
}
