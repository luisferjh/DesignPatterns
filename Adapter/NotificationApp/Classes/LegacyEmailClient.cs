namespace NotificationApp.Classes;

public class LegacyEmailClient
{
    public void SendMail(string to, string subject, string body)
    {
        Console.WriteLine($"Sending email to {to} with subject '{subject}' and body '{body}'");
    }
}


