using Reservation.Api.Enums;

namespace Reservation.Api.Models;

public record ReservationResponse(
    Guid Id,
    Guid ResourceId,
    string CustomerName,
    DateTimeOffset StartTime,
    DateTimeOffset EndTime,
    ReservationStatus Status,
    DateTimeOffset ConfirmationDeadline,
    DateTimeOffset CreatedAt,
    DateTimeOffset? ConfirmedAt = null,
    DateTimeOffset? CancelledAt = null);
