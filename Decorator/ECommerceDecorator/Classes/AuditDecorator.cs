using System.Text.Json;
using ECommerceDecorator.Interfaces;
using ECommerceDecorator.Models;

namespace ECommerceDecorator.Classes;

// Solo entra en la cadena de clientes corporativos.
// Se apila dos veces: una antes del primer paso y otra al final,
// capturando el estado del pedido en ambos momentos.
public class AuditDecorator : OrderProcessorDecorator
{
    private readonly IAuditService _auditService;
    private readonly string _stage;

    public AuditDecorator(IOrderProcessor inner, IAuditService auditService, string stage)
        : base(inner)
    {
        _auditService = auditService;
        _stage = stage;
    }

    public override async Task<Order> ProcessAsync(Order order)
    {
        // Captura snapshot ANTES de delegar al inner
        var snapshot = new
        {
            order.Id,
            order.Subtotal,
            order.Total,
            order.Currency,
            Stage = _stage,
            Timestamp = DateTime.UtcNow
        };

        await _auditService.LogAsync(order.Id.ToString(), _stage, snapshot);
        order.ProcessingLog.Add($"[Auditoría] Snapshot '{_stage}' registrado.");

        return await _inner.ProcessAsync(order);
    }
}