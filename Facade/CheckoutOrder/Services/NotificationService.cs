using CheckoutOrder.Interfaces;

namespace CheckoutOrder.Services;

public class NotificationService : INotificationService
{
    public void SendOrderConfirmation(string customerId, string transactionId, string trackingCarrier, int estimatedDays)
    {
        // Simulación: en un proyecto real aquí iría la integración con
        // un proveedor de email (SendGrid, SES, etc.) o una cola de mensajería.
        Console.WriteLine(
            $"[Notificación] Cliente '{customerId}': tu pedido fue confirmado. " +
            $"Transacción: {transactionId}. Envío por {trackingCarrier}, " +
            $"llega en aproximadamente {estimatedDays} día(s).");
    }
}