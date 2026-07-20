namespace PaymentProcessor.Services;

public class DailyLimitTracker
{
    private readonly Dictionary<string, decimal> _totals = new(StringComparer.OrdinalIgnoreCase);

    public decimal GetTotal(string merchantId)
    {
        return _totals.TryGetValue(merchantId, out var total) ? total : 0m;
    }

    public void AddCharge(string merchantId, decimal amount)
    {
        if (_totals.TryGetValue(merchantId, out var total))
        {
            _totals[merchantId] = total + amount;
            return;
        }

        _totals[merchantId] = amount;
    }
}
