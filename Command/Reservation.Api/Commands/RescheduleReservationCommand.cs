using Reservation.Api.Interfaces;
using Reservation.Api.Models;

namespace Reservation.Api.Commands;

public class RescheduleReservationCommand(Guid reservationId, RescheduleReservationRequest request, IReservationService service) : ICommand
{
    // Null until Execute has run.
    public OperationResult<ReservationResponse>? Result { get; private set; }

    public async Task Execute()
    {
        Result = await service.RescheduleAsync(reservationId, request, DateTimeOffset.UtcNow);
    }
}