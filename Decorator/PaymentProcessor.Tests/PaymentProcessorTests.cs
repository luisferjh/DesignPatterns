using Microsoft.Extensions.Logging.Abstractions;
using PaymentProcessor.Decorators;
using PaymentProcessor.Factory;
using PaymentProcessor.Interfaces;
using PaymentProcessor.Models;
using PaymentProcessor.Services;
using PaymentProcessorImplementation = PaymentProcessor.PaymentProcessor;

namespace PaymentProcessor.Tests;

public class PaymentProcessorTests
{
    [Fact]
    public void LimitValidationDecorator_Rejects_WhenDailyLimitIsExceeded()
    {
        var tracker = new DailyLimitTracker();
        var config = new MerchantConfig
        {
            MerchantId = "A",
            DailyLimit = 100m,
            EnableLimitValidation = true,
            EnableLogging = false,
            EnableCardMasking = false
        };

        IPaymentProcessor processor = new LimitValidationDecorator(
            new PaymentProcessorImplementation(),
            config,
            tracker);

        var first = processor.ProcessPayment(CreateRequest(60m, "A"));
        var second = processor.ProcessPayment(CreateRequest(60m, "A"));

        Assert.Equal("Authorized", first.Status);
        Assert.Equal("Rejected", second.Status);
        Assert.Equal("Daily limit exceeded", second.TransactionId);
    }

    [Fact]
    public void CardMaskingDecorator_MasksCardToken_InResponse()
    {
        IPaymentProcessor processor = new CardMaskingDecorator(
            new PaymentProcessorImplementation());

        var result = processor.ProcessPayment(CreateRequest(25m, "B", "4111111111114321"));

        Assert.Equal("****-****-****-4321", result.MaskedCard);
    }

    [Fact]
    public void MerchantProcessorFactory_BuildsPipeline_ForMerchantWithMasking()
    {
        var factory = new MerchantProcessorFactory(
            NullLoggerFactory.Instance,
            new DailyLimitTracker());

        var config = new MerchantConfig
        {
            MerchantId = "B",
            DailyLimit = 200m,
            EnableLogging = true,
            EnableLimitValidation = false,
            EnableCardMasking = true
        };

        var processor = factory.Create(config);
        var result = processor.ProcessPayment(CreateRequest(50m, "B", "4111111111114321"));

        Assert.Equal("Authorized", result.Status);
        Assert.Equal("****-****-****-4321", result.MaskedCard);
    }

    private static PaymentRequest CreateRequest(decimal amount, string merchantId, string cardToken = "4111111111111111")
        => new()
        {
            Amount = amount,
            Currency = "USD",
            MerchantId = merchantId,
            CardToken = cardToken
        };
}
