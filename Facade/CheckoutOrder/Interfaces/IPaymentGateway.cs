using CheckoutOrder.Models;

namespace CheckoutOrder.Interfaces;

public interface IPaymentGateway
{
    /// <summary>
    /// Intenta cobrar el monto indicado. Simula un proveedor de pagos
    /// externo que puede fallar por razones ajenas a la aplicación
    /// (fondos insuficientes, gateway caído, etc.).
    /// </summary>
    PaymentResult Charge(decimal amount, string customerId);
}