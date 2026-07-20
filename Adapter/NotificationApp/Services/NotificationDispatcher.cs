using NotificationApp.Interfaces;
using NotificationApp.Models;

namespace NotificationApp.Services;
public class NotificationDispatcher
{
    private readonly IServiceProvider _provider;

    public NotificationDispatcher(IServiceProvider provider) => _provider = provider;

    public Task<NotificationResult> DispatchAsync(NotificationRequest request)
    {
        var service = _provider.GetRequiredKeyedService<INotificationSender>(request.Channel);
        return service.SendAsync(request);
    }
}

