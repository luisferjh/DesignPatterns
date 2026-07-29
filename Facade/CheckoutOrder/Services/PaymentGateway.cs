using CheckoutOrder.Interfaces;
using CheckoutOrder.Models;

namespace CheckoutOrder.Services;

 public class PaymentGateway : IPaymentGateway
    {
        private readonly Random _random = new();
 
        public PaymentResult Charge(decimal amount, string customerId)
        {
            if (amount <= 0)
            {
                return new PaymentResult
                {
                    Success = false,
                    FailureReason = "El monto a cobrar debe ser mayor que cero."
                };
            }
 
            // Simulación: ~10% de probabilidad de que el pago sea rechazado,
            // para forzar el manejo de rollback en la fachada.
            var isDeclined = _random.Next(1, 101) <= 10;
 
            if (isDeclined)
            {
                return new PaymentResult
                {
                    Success = false,
                    FailureReason = "Pago rechazado por el proveedor de pagos."
                };
            }
 
            return new PaymentResult
            {
                Success = true,
                TransactionId = $"TXN-{Guid.NewGuid():N}"[..12]
            };
        }
    }