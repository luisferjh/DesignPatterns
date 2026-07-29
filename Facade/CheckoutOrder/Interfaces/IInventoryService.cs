using CheckoutOrder.Models;

namespace CheckoutOrder.Interfaces;

public interface IInventoryService
{
        /// <summary>
        /// Intenta reservar stock para todos los ítems del pedido.
        /// Si CUALQUIER ítem no tiene stock suficiente, la reserva completa falla
        /// (no deja reservas parciales colgadas).
        /// </summary>
        ReservationResult ReserveStock(List<OrderItem> items);
 
        /// <summary>
        /// Libera una reserva previamente creada. Se usa para hacer rollback
        /// cuando un paso posterior del checkout (ej. el pago) falla.
        /// </summary>
        void ReleaseReservation(string reservationId);
}