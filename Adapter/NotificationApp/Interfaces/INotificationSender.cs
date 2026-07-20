using NotificationApp.Models;

namespace NotificationApp.Interfaces;

public interface INotificationSender
{
    Task<NotificationResult> SendAsync(NotificationRequest request);
}