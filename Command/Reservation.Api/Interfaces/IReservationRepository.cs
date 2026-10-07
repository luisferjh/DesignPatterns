using Reservation.Api.Models;
using ReservationEntity = Reservation.Api.Models.Reservation;

namespace Reservation.Api.Interfaces;

public interface IReservationRepository
{
    Task<Resource?> GetActiveResourceAsync(Guid resourceId);
    Task<bool> HasOverlapAsync(Guid resourceId, DateTimeOffset start, DateTimeOffset end, Guid? excludeReservationId = null);
    Task AddAsync(ReservationEntity reservation);
    Task<ReservationEntity?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<ReservationEntity>> GetAllAsync();
    Task UpdateAsync(ReservationEntity reservation);
}
