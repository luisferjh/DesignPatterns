namespace Reservation.Api.Models;

public record CreateReservationRequest(
    Guid ResourceId,
    string? CustomerName,
    DateTimeOffset StartTime,
    DateTimeOffset EndTime);
