using Microsoft.EntityFrameworkCore;
using Reservation.Api.Data;
using Reservation.Api.Enums;
using Reservation.Api.Interfaces;
using Reservation.Api.Models;
using ReservationEntity = Reservation.Api.Models.Reservation;

namespace Reservation.Api.Repositories;

public class ReservationRepository(ReservationDbContext db) : IReservationRepository
{
    public Task<Resource?> GetActiveResourceAsync(Guid resourceId) =>
        db.Resources.FirstOrDefaultAsync(r => r.Id == resourceId && r.IsActive);

    // Callers must pass UTC values; SQLite compares the stored binary representation.
    public Task<bool> HasOverlapAsync(Guid resourceId, DateTimeOffset start, DateTimeOffset end, Guid? excludeReservationId = null) =>
        db.Reservations.AnyAsync(r =>
            r.ResourceId == resourceId
            && r.Id != excludeReservationId
            && r.Status != ReservationStatus.Cancelled
            && r.StartTime < end
            && r.EndTime > start);

    public async Task AddAsync(ReservationEntity reservation)
    {
        db.Reservations.Add(reservation);
        await db.SaveChangesAsync();
    }

    public Task<ReservationEntity?> GetByIdAsync(Guid id) =>
        db.Reservations.FirstOrDefaultAsync(r => r.Id == id);

    public async Task<IReadOnlyList<ReservationEntity>> GetAllAsync() =>
        await db.Reservations.AsNoTracking().OrderBy(r => r.StartTime).ToListAsync();

    // Entities from GetByIdAsync are tracked, so saving persists their changes.
    public Task UpdateAsync(ReservationEntity reservation) => db.SaveChangesAsync();
}
