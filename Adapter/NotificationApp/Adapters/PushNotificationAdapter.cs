using NotificationApp.Classes;
using NotificationApp.Interfaces;
using NotificationApp.Models;

namespace NotificationApp.Adapters;

public class PushNotificationAdapter : INotificationSender
{
    private readonly PushNotificationService _pushNotificationService;
    public PushNotificationAdapter(PushNotificationService pushNotificationService)
    {
        _pushNotificationService = pushNotificationService;
    }

    public async Task<NotificationResult> SendAsync(NotificationRequest request)
    {
        var payload = new PushPayload
        {
            DeviceId = request.Recipient,
            Title = request.Subject,
            Text = request.Message
        };

        var result = await _pushNotificationService.PushAsync(payload);
        return new NotificationResult { Success = result.Delivered };
    }
}