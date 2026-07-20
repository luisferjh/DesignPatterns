namespace NotificationApp.Models;

public class NotificationResult
{
    public bool Success { get; set; }
    public string? Channel { get; set; }      // útil para logging/trazabilidad
    public string? ProviderReference { get; set; } // id/código que devuelva el proveedor si aplica (ej. ErrorCode de Push)
    public string? ErrorMessage { get; set; } // mensaje normalizado si Success = false
}