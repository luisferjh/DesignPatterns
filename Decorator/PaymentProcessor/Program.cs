using PaymentProcessor.Factory;
using PaymentProcessor.Models;
using PaymentProcessor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<DailyLimitTracker>();
builder.Services.AddSingleton<MerchantProcessorFactory>();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapPost("/api/payments", (PaymentRequest request, MerchantProcessorFactory factory) =>
{
    var merchantConfig = ResolveMerchantConfig(request.MerchantId);
    var processor = factory.Create(merchantConfig);
    return Results.Ok(processor.ProcessPayment(request));
});

app.Run();

static MerchantConfig ResolveMerchantConfig(string merchantId) => merchantId switch
{
    "A" => new MerchantConfig { MerchantId = "A", DailyLimit = 1000m, EnableLogging = true, EnableLimitValidation = true, EnableCardMasking = false },
    "B" => new MerchantConfig { MerchantId = "B", DailyLimit = 1000m, EnableLogging = true, EnableLimitValidation = false, EnableCardMasking = true },
    "Premium" => new MerchantConfig { MerchantId = "Premium", DailyLimit = 5000m, EnableLogging = true, EnableLimitValidation = true, EnableCardMasking = true },
    _ => new MerchantConfig { MerchantId = merchantId, DailyLimit = 1000m, EnableLogging = false, EnableLimitValidation = false, EnableCardMasking = false }
};
