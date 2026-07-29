using CheckoutOrder.Interfaces;
using CheckoutOrder.Models;

namespace CheckoutOrder.Services;

public class InventoryService : IInventoryService
{
    // Simulación de un almacén en memoria: productId -> stock disponible.
    private readonly Dictionary<string, int> _stock = new()
    {
        { "SKU-001", 50 },
        { "SKU-002", 5 },
        { "SKU-003", 100 },
        { "SKU-004", 0 },
    };
 
    // Reservas activas: reservationId -> lista de (productId, cantidad reservada).
    private readonly Dictionary<string, List<(string ProductId, int Quantity)>> _activeReservations = new();

    public ReservationResult ReserveStock(List<OrderItem> items)
    {
        // 1. Validar que hay stock suficiente para TODOS los ítems antes de reservar nada.
        foreach (var item in items)
        {
            if (!_stock.TryGetValue(item.ProductId, out var available) || available < item.Quantity)
            {
                return new ReservationResult
                {
                    Success = false,
                    FailureReason = $"Stock insuficiente para el producto '{item.ProductId}'."
                };
            }
        }

        // 2. Si todo tiene stock, se descuenta y se registra la reserva.
        var reservationId = Guid.NewGuid().ToString();
        var reservedItems = new List<(string, int)>();

        foreach (var item in items)
        {
            _stock[item.ProductId] -= item.Quantity;
            reservedItems.Add((item.ProductId, item.Quantity));
        }

        _activeReservations[reservationId] = reservedItems;

        return new ReservationResult
        {
            Success = true,
            ReservationId = reservationId
        };
    }

    public void ReleaseReservation(string reservationId)
    {
        if (!_activeReservations.TryGetValue(reservationId, out var reservedItems))
        {
            // Reserva inexistente o ya liberada: no hacemos nada (idempotente).
            return;
        }

        // Devolvemos el stock reservado.
        foreach (var (productId, quantity) in reservedItems)
        {
            _stock[productId] += quantity;
        }

        _activeReservations.Remove(reservationId);
    }

}
