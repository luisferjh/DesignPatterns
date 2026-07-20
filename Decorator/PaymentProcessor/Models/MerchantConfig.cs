namespace PaymentProcessor.Models;

public class MerchantConfig
{
    public string MerchantId { get; set; } = string.Empty;
    public decimal DailyLimit { get; set; }
    public bool EnableLogging { get; set; }
    public bool EnableLimitValidation { get; set; }
    public bool EnableCardMasking { get; set; }
}
