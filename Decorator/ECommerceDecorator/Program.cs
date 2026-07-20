using ECommerceDecorator.Classes;
using ECommerceDecorator.Factory;
using ECommerceDecorator.Interfaces;
using ECommerceDecorator.Models;
using ECommerceDecorator.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// ── Servicios externos (stubs) ─────────────────────────────────────
builder.Services.AddScoped<IStockService,        StubStockService>();
builder.Services.AddScoped<IExchangeRateService, StubExchangeRateService>();
builder.Services.AddScoped<IEmailService,        StubEmailService>();
builder.Services.AddScoped<IAuditService,        StubAuditService>();
 
// ── Factory: único punto que conoce los flujos ─────────────────────
builder.Services.AddScoped<OrderProcessorFactory>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPost("/orders", async (Order order, OrderProcessorFactory factory) =>
{
    var processor = factory.Create(order.Customer.Type, order.DestinationCountry);
    var processed = await processor.ProcessAsync(order);
    return Results.Ok(processed);
});

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
