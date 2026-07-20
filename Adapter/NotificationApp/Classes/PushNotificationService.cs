using NotificationApp.Models;

namespace NotificationApp.Classes;
public class PushNotificationService
{
    public Task<PushResult> PushAsync(PushPayload payload)
    {
        // Simulación de envío de notificación push
        Console.WriteLine($"Sending push notification to {payload.DeviceId} with title '{payload.Title}' and message '{payload.Text}'");
        return Task.FromResult(new PushResult { Delivered = true });
    }
}