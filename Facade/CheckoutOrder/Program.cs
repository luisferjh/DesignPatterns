using CheckoutOrder.Classes;
using CheckoutOrder.Interfaces;
using CheckoutOrder.Models;
using CheckoutOrder.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddScoped<IShippingService, ShippingService>();
builder.Services.AddScoped<IPaymentGateway, PaymentGateway>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<IPricingService, PricingService>();
builder.Services.AddScoped<OrderCheckoutFacade>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// crear endpoint para simular la confirmación de un pedido
// ruta cambiada a /api/checkout
app.MapPost("/api/checkout", (
    CheckoutRequest request, OrderCheckoutFacade facade) =>
{
    CheckoutResult result = facade.CheckoutOrder(request);

    if (!result.Success)
        return Results.BadRequest(new { message = result.FailureReason });    

    // Simular la confirmación de un pedido
    return Results.Ok(result);
}); 

app.Run();

