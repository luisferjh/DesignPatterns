namespace NotificationApp.Classes;

public class ThirdPartySmsGateway
{
    public bool Dispatch(string phoneNumber, string message, int priority)
    {
        // Simulación de envío de SMS
        Console.WriteLine($"Sending SMS to {phoneNumber} with message '{message}' and priority {priority}");
        return true;
    }
}
