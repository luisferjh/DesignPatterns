namespace PaymentProcessor.Models;

public class PaymentResult
{
    public string TransactionId { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public DateTime Timestamp { get; set; }
    public string MaskedCard { get; set; } = string.Empty;
}
