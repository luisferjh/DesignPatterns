using Reservation.Api.Enums;
using Reservation.Api.Exceptions;
using Reservation.Api.Interfaces;
using Reservation.Api.Models;
using ReservationEntity = Reservation.Api.Models.Reservation;

namespace Reservation.Api.Services;

public class ReservationService(IReservationRepository repository) : IReservationService
{
    public async Task<OperationResult<ReservationResponse>> CreateAsync(CreateReservationRequest request, DateTimeOffset now)
    {
        ReservationEntity reservation;
        try
        {
            reservation = ReservationEntity.Create(request, now);
        }
        catch (ReservationDomainException ex)
        {
            return OperationResult<ReservationResponse>.ValidationFailed(ex.Message);
        }

        if (await repository.GetActiveResourceAsync(reservation.ResourceId) is null)
            return OperationResult<ReservationResponse>.NotFound("Resource not found or inactive.");

        if (await repository.HasOverlapAsync(reservation.ResourceId, reservation.StartTime, reservation.EndTime))
            return OperationResult<ReservationResponse>.Conflict("The resource is already reserved for that time range.");

        await repository.AddAsync(reservation);

        return OperationResult<ReservationResponse>.Success(ToResponse(reservation));
    }

    public async Task<OperationResult<ReservationResponse>> CancelAsync(Guid reservationId, DateTimeOffset now)
    {
        var reservation = await repository.GetByIdAsync(reservationId);
        if (reservation is null)
            return OperationResult<ReservationResponse>.NotFound("Reservation not found.");

        try
        {
            reservation.Cancel(now);
        }
        catch (ReservationDomainException ex)
        {
            return OperationResult<ReservationResponse>.Conflict(ex.Message);
        }

        await repository.UpdateAsync(reservation);

        return OperationResult<ReservationResponse>.Success(ToResponse(reservation));
    }

    public async Task<OperationResult<ReservationResponse>> RescheduleAsync(
        Guid reservationId, RescheduleReservationRequest request, DateTimeOffset now)
    {
        var reservation = await repository.GetByIdAsync(reservationId);
        if (reservation is null)
            return OperationResult<ReservationResponse>.NotFound("Reservation not found.");

        if (reservation.Status == ReservationStatus.Cancelled)
            return OperationResult<ReservationResponse>.Conflict("A cancelled reservation cannot be rescheduled.");

        try
        {
            reservation.Reschedule(request.StartTime, request.EndTime, now);
        }
        catch (ReservationDomainException ex)
        {
            return OperationResult<ReservationResponse>.ValidationFailed(ex.Message);
        }

        // Nothing is saved on conflict; the scoped context is discarded with the tracked changes.
        if (await repository.HasOverlapAsync(reservation.ResourceId, reservation.StartTime, reservation.EndTime, reservation.Id))
            return OperationResult<ReservationResponse>.Conflict("The resource is already reserved for that time range.");

        await repository.UpdateAsync(reservation);

        return OperationResult<ReservationResponse>.Success(ToResponse(reservation));
    }

    public async Task<OperationResult<ReservationResponse>> ConfirmAsync(Guid reservationId, DateTimeOffset now)
    {
        var reservation = await repository.GetByIdAsync(reservationId);
        if (reservation is null)
            return OperationResult<ReservationResponse>.NotFound("Reservation not found.");

        try
        {
            reservation.Confirm(now);
        }
        catch (ReservationDomainException ex)
        {
            return OperationResult<ReservationResponse>.Conflict(ex.Message);
        }

        await repository.UpdateAsync(reservation);

        return OperationResult<ReservationResponse>.Success(ToResponse(reservation));
    }

    public async Task<IReadOnlyList<ReservationResponse>> ListAsync() =>
        (await repository.GetAllAsync()).Select(ToResponse).ToList();

    private static ReservationResponse ToResponse(ReservationEntity reservation) => new(
        reservation.Id,
        reservation.ResourceId,
        reservation.CustomerName,
        reservation.StartTime,
        reservation.EndTime,
        reservation.Status,
        reservation.ConfirmationDeadline,
        reservation.CreatedAt,
        reservation.ConfirmedAt,
        reservation.CancelledAt);
}
