using Reservation.Api.Enums;

namespace Reservation.Api.Models;

public class AuditEntry
{
    public Guid Id { get; set; }
    public string CommandName { get; set; } = string.Empty;
    // Deliberately not a foreign key so audit rows outlive reservations and failed attempts.
    public Guid? ReservationId { get; set; }
    public string Parameters { get; set; } = "{}";
    public DateTimeOffset ExecutedAt { get; set; }
    public CommandStatus Status { get; set; }
    public string? ErrorMessage { get; set; }
}
