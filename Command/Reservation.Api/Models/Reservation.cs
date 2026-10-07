using Reservation.Api.Enums;
using Reservation.Api.Exceptions;

namespace Reservation.Api.Models;

public class Reservation
{
    public static readonly TimeSpan ConfirmationWindow = TimeSpan.FromHours(24);

    public Guid Id { get; set; }
    public Guid ResourceId { get; set; }
    public Resource? Resource { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public ReservationStatus Status { get; set; } = ReservationStatus.Pending;
    public DateTimeOffset ConfirmationDeadline { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? ConfirmedAt { get; set; }
    public DateTimeOffset? CancelledAt { get; set; }

    public static Reservation Create(CreateReservationRequest createReservationRequest, DateTimeOffset now)
    {
        if (string.IsNullOrWhiteSpace(createReservationRequest.CustomerName))
            throw new ReservationDomainException("CustomerName is required.");
        ValidateTimeRange(createReservationRequest.StartTime, createReservationRequest.EndTime, now);

        var startUtc = createReservationRequest.StartTime.ToUniversalTime();
        return new Reservation
        {
            Id = Guid.NewGuid(),
            ResourceId = createReservationRequest.ResourceId,
            CustomerName = createReservationRequest.CustomerName.Trim(),
            StartTime = startUtc,
            EndTime = createReservationRequest.EndTime.ToUniversalTime(),
            Status = ReservationStatus.Pending,
            ConfirmationDeadline = startUtc - ConfirmationWindow,
            CreatedAt = now.ToUniversalTime()
        };
    }

    public void Cancel(DateTimeOffset now)
    {
        if (Status == ReservationStatus.Cancelled)
            throw new ReservationDomainException("The reservation is already cancelled.");

        Status = ReservationStatus.Cancelled;
        CancelledAt = now.ToUniversalTime();
    }

    public void Confirm(DateTimeOffset now)
    {
        if (Status != ReservationStatus.Pending)
            throw new ReservationDomainException($"Only pending reservations can be confirmed (current status: {Status}).");
        if (now > ConfirmationDeadline)
            throw new ReservationDomainException("The confirmation deadline has passed.");

        Status = ReservationStatus.Confirmed;
        ConfirmedAt = now.ToUniversalTime();
    }

    public void Reschedule(DateTimeOffset newStart, DateTimeOffset newEnd, DateTimeOffset now)
    {
        if (Status == ReservationStatus.Cancelled)
            throw new ReservationDomainException("A cancelled reservation cannot be rescheduled.");
        ValidateTimeRange(newStart, newEnd, now);

        StartTime = newStart.ToUniversalTime();
        EndTime = newEnd.ToUniversalTime();
        ConfirmationDeadline = StartTime - ConfirmationWindow;

        // The previous confirmation applied to the old time slot.
        if (Status == ReservationStatus.Confirmed)
        {
            Status = ReservationStatus.Pending;
            ConfirmedAt = null;
        }
    }

    private static void ValidateTimeRange(DateTimeOffset start, DateTimeOffset end, DateTimeOffset now)
    {
        if (end <= start)
            throw new ReservationDomainException("EndTime must be after StartTime.");
        if (start <= now)
            throw new ReservationDomainException("StartTime must be in the future.");
    }
}
