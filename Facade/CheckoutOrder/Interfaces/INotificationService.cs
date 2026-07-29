namespace CheckoutOrder.Interfaces;

public interface INotificationService
{
    /// <summary>
    /// Envía (de forma simulada) una notificación de confirmación al cliente.
    /// En un caso real, esto delegaría a un servicio de correo/SMS/push.
    /// </summary>
    void SendOrderConfirmation(string customerId, string transactionId, string trackingCarrier, int estimatedDays);
}