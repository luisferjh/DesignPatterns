using CheckoutOrder.Interfaces;
using CheckoutOrder.Models;

namespace CheckoutOrder.Classes;

public class OrderCheckoutFacade
{
    private readonly IInventoryService _inventoryService;
    private readonly IShippingService _shippingService;
    private readonly IPaymentGateway _paymentGateway;
    private readonly INotificationService _notificationService;
    private readonly IPricingService _pricingService;

    public OrderCheckoutFacade(
        IInventoryService inventoryService,
        IShippingService shippingService,
        IPaymentGateway paymentGateway,
        INotificationService notificationService,
        IPricingService pricingService)
    {
        _inventoryService = inventoryService;
        _shippingService = shippingService;
        _paymentGateway = paymentGateway;
        _notificationService = notificationService;
        _pricingService = pricingService;
    }

    // Aquí se podrían agregar métodos que combinen las operaciones de los servicios
    // para simplificar el proceso de checkout para el cliente.
    public CheckoutResult CheckoutOrder(CheckoutRequest request)
    {
        //inventory
        ReservationResult reservation = _inventoryService.ReserveStock(request.Items);

        if (!reservation.Success)
        {
            return new CheckoutResult
            {
                Success = false,
                FailureReason = $"No se pudo reservar stock: {reservation.FailureReason}"
            };
        }

        PricingResult pricingResult = _pricingService.CalculateTotal(request.Items, request.CouponCode);

        //payment
        PaymentResult  paymentResult = _paymentGateway.Charge(pricingResult.Total, request.CustomerId);

        if (!paymentResult.Success)
        {
            // Rollback: liberar la reserva de stock
            _inventoryService.ReleaseReservation(reservation.ReservationId!);

            return new CheckoutResult
            {
                Success = false,
                FailureReason = $"El pago falló: {paymentResult.FailureReason}"
            };
        }

        // shiping
        ShippingQuote shippingResult = _shippingService.CalculateShipping(request.ShippingAddress, request.Items.Count);

        //notification
        _notificationService.SendOrderConfirmation(request.CustomerId, paymentResult.TransactionId!, shippingResult.Carrier, shippingResult.EstimatedDays);

        return new CheckoutResult
        {
            Success = true,
            TransactionId = paymentResult.TransactionId,
            ShippingCarrier = shippingResult.Carrier,
            EstimatedDeliveryDays = shippingResult.EstimatedDays,
            TotalCharged = pricingResult.Total,
            DiscountApplied = pricingResult.DiscountApplied,
        };

    }
}