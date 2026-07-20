using NotificationApp.Classes;
using NotificationApp.Interfaces;
using NotificationApp.Models;

namespace NotificationApp.Adapters;

public class EmailNotificationAdapter : INotificationSender
{
    private readonly LegacyEmailClient _legacyEmailClient;
    public EmailNotificationAdapter(LegacyEmailClient legacyEmailClient)
    {
        _legacyEmailClient = legacyEmailClient;
    }

    public Task<NotificationResult> SendAsync(NotificationRequest request)
    {
        _legacyEmailClient.SendMail(request.Recipient, request.Subject, request.Message);
        return Task.FromResult(new NotificationResult { Success = true });
    }
}