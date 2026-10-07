using Reservation.Api.Interfaces;
using Reservation.Api.Models;

namespace Reservation.Api.Commands;

public class CancelReservationCommand(Guid reservationId, IReservationService service) : ICommand
{
    // Null until Execute has run.
    public OperationResult<ReservationResponse>? Result { get; private set; }

    public async Task Execute()
    {
        Result = await service.CancelAsync(reservationId, DateTimeOffset.UtcNow);
    }
}