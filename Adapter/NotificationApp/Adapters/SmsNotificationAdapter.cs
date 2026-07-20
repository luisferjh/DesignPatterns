using NotificationApp.Classes;
using NotificationApp.Interfaces;
using NotificationApp.Models;

namespace NotificationApp.Adapters;

public class SmsNotificationAdapter : INotificationSender
{
    private readonly ThirdPartySmsGateway _thirdPartySmsGateway;
    public SmsNotificationAdapter(ThirdPartySmsGateway thirdPartySmsGateway)
    {
        _thirdPartySmsGateway = thirdPartySmsGateway;
    }

    public Task<NotificationResult> SendAsync(NotificationRequest request)
    {
        var success = _thirdPartySmsGateway.Dispatch(request.Recipient, request.Message, 1);
        return Task.FromResult(new NotificationResult { Success = success });
    }
}