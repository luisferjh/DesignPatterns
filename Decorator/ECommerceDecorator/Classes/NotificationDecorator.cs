

using ECommerceDecorator.Interfaces;
using ECommerceDecorator.Models;

namespace ECommerceDecorator.Classes;

public class NotificationDecorator : OrderProcessorDecorator
{
    private readonly IEmailService _emailService;

    public NotificationDecorator(IOrderProcessor inner, IEmailService emailService)
        : base(inner)
    {
        _emailService = emailService;
    }

    public override async Task<Order> ProcessAsync(Order order)
    {
        order = await _inner.ProcessAsync(order);

        await _emailService.SendAsync(
            to: order.Customer.Email,
            subject: $"Tu pedido #{order.Id} fue procesado",
            body: $"Hola {order.Customer.Name}, tu pedido fue procesado exitosamente. Total: {order.Total} {order.Currency}."
        );

        order.ProcessingLog.Add($"[Notificación] Email enviado a {order.Customer.Email}.");

        return order;
    }
}