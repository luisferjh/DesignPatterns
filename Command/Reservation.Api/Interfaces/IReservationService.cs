using Reservation.Api.Models;

namespace Reservation.Api.Interfaces;

public interface IReservationService
{
    Task<OperationResult<ReservationResponse>> CreateAsync(CreateReservationRequest request, DateTimeOffset now);
    Task<OperationResult<ReservationResponse>> CancelAsync(Guid reservationId, DateTimeOffset now);
    Task<OperationResult<ReservationResponse>> RescheduleAsync(Guid reservationId, RescheduleReservationRequest request, DateTimeOffset now);
    Task<OperationResult<ReservationResponse>> ConfirmAsync(Guid reservationId, DateTimeOffset now);
    Task<IReadOnlyList<ReservationResponse>> ListAsync();
}
