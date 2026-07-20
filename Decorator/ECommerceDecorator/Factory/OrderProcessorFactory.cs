using ECommerceDecorator.Classes;
using ECommerceDecorator.Interfaces;

namespace ECommerceDecorator.Factory;

// Único lugar del sistema donde se sabe cuántos pasos hay,
// en qué orden van y qué combinaciones existen.
// El controller y los decoradores son completamente ajenos a esto.
public class OrderProcessorFactory
{
    private readonly IStockService _stockService;
    private readonly IExchangeRateService _exchangeRateService;
    private readonly IEmailService _emailService;
    private readonly IAuditService _auditService;

    private const string LocalCountry = "CO";
    private const decimal RegularDiscount = 5m;
    private const decimal PremiumDiscount = 10m;
    private const decimal CorporateDiscount = 15m;
    private const decimal DomesticTax = 19m;
    private const decimal InternationalTax = 5m;

    public OrderProcessorFactory(
        IStockService stockService,
        IExchangeRateService exchangeRateService,
        IEmailService emailService,
        IAuditService auditService)
    {
        _stockService = stockService;
        _exchangeRateService = exchangeRateService;
        _emailService = emailService;
        _auditService = auditService;
    }

    public IOrderProcessor Create(CustomerType customerType, string destinationCountry)
    {
        bool isInternational = !destinationCountry.Equals(LocalCountry, StringComparison.OrdinalIgnoreCase);

        return customerType switch
        {
            CustomerType.Regular    => BuildRegular(isInternational),
            CustomerType.Premium    => BuildPremium(isInternational),
            CustomerType.Corporate  => BuildCorporate(isInternational),
            _ => throw new ArgumentOutOfRangeException(nameof(customerType))
        };
    }

    // ── Flujo Regular ──────────────────────────────────────────────
    // descuento → stock → [moneda si intl] → impuestos → notificación
    private IOrderProcessor BuildRegular(bool isInternational)
    {
        IOrderProcessor pipeline = new RawOrderProcessor();
        pipeline = new DiscountDecorator(pipeline, RegularDiscount);
        pipeline = new StockValidationDecorator(pipeline, _stockService);
        if (isInternational)
            pipeline = new CurrencyConversionDecorator(pipeline, _exchangeRateService);
        pipeline = new TaxDecorator(pipeline, isInternational ? InternationalTax : DomesticTax,
                                    isInternational ? "Arancel" : "IVA");
        pipeline = new NotificationDecorator(pipeline, _emailService);
        return pipeline;
    }

    // ── Flujo Premium ──────────────────────────────────────────────
    // descuento mayor → stock → [moneda si intl] → impuestos → notificación
    private IOrderProcessor BuildPremium(bool isInternational)
    {
        IOrderProcessor pipeline = new RawOrderProcessor();
        pipeline = new DiscountDecorator(pipeline, PremiumDiscount);
        pipeline = new StockValidationDecorator(pipeline, _stockService);
        if (isInternational)
            pipeline = new CurrencyConversionDecorator(pipeline, _exchangeRateService);
        pipeline = new TaxDecorator(pipeline, isInternational ? InternationalTax : DomesticTax,
                                    isInternational ? "Arancel" : "IVA");
        pipeline = new NotificationDecorator(pipeline, _emailService);
        return pipeline;
    }

    // ── Flujo Corporativo ──────────────────────────────────────────
    // auditoría-entrada → descuento → stock → [moneda si intl] → impuestos → notificación → auditoría-salida
    private IOrderProcessor BuildCorporate(bool isInternational)
    {
        IOrderProcessor pipeline = new RawOrderProcessor();
        pipeline = new DiscountDecorator(pipeline, CorporateDiscount);
        pipeline = new StockValidationDecorator(pipeline, _stockService);
        if (isInternational)
            pipeline = new CurrencyConversionDecorator(pipeline, _exchangeRateService);
        pipeline = new TaxDecorator(pipeline, isInternational ? InternationalTax : DomesticTax,
                                    isInternational ? "Arancel" : "IVA");
        pipeline = new NotificationDecorator(pipeline, _emailService);
        pipeline = new AuditDecorator(pipeline, _auditService, "post-procesamiento");
        pipeline = new AuditDecorator(pipeline, _auditService, "pre-procesamiento");
        return pipeline;
    }
}