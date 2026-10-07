namespace Reservation.Api.Models;

public record RescheduleReservationRequest(DateTimeOffset StartTime, DateTimeOffset EndTime);
