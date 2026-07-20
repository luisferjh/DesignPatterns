namespace NotificationApp.Models;

public class NotificationRequest
{
    public string Channel { get; set; }      // "email" | "sms" | "push" — lo usa el factory, no el adapter
    public string Recipient { get; set; }     // email, número de teléfono o deviceId según el canal
    public string Message { get; set; }       // cuerpo del mensaje
    public string? Subject { get; set; }      // opcional, solo lo usa el adapter de email (subject)
}