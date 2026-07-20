using Microsoft.Extensions.Logging;
using PaymentProcessor.Decorators;
using PaymentProcessor.Interfaces;
using PaymentProcessor.Models;
using PaymentProcessor.Services;
using PaymentProcessorImplementation = PaymentProcessor.PaymentProcessor;

namespace PaymentProcessor.Factory;

public class MerchantProcessorFactory
{
    private readonly ILoggerFactory _loggerFactory;
    private readonly DailyLimitTracker _tracker;

    public MerchantProcessorFactory(ILoggerFactory loggerFactory, DailyLimitTracker tracker)
    {
        _loggerFactory = loggerFactory;
        _tracker = tracker;
    }

    public IPaymentProcessor Create(MerchantConfig config)
    {
        IPaymentProcessor processor = new PaymentProcessorImplementation();

        if (config.EnableLimitValidation)
        {
            processor = new LimitValidationDecorator(processor, config, _tracker);
        }

        if (config.EnableLogging)
        {
            processor = new LoggingDecorator(processor, _loggerFactory.CreateLogger<LoggingDecorator>());
        }

        if (config.EnableCardMasking)
        {
            processor = new CardMaskingDecorator(processor);
        }

        return processor;
    }
}
