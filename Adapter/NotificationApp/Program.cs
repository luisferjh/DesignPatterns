using NotificationApp.Adapters;
using NotificationApp.Classes;
using NotificationApp.Interfaces;
using NotificationApp.Models;
using NotificationApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<NotificationDispatcher>();

builder.Services.AddTransient<LegacyEmailClient>();
builder.Services.AddTransient<ThirdPartySmsGateway>();
builder.Services.AddTransient<PushNotificationService>();

builder.Services.AddKeyedScoped<INotificationSender, EmailNotificationAdapter>("email");
builder.Services.AddKeyedScoped<INotificationSender, PushNotificationAdapter>("push");
builder.Services.AddKeyedScoped<INotificationSender, SmsNotificationAdapter>("sms");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection(); 

app.MapPost("/notifications", async  (
    NotificationRequest  request,
    NotificationDispatcher dispatcher) =>
{
   var result = await dispatcher.DispatchAsync(request);
    return result.Success ? Results.Ok(result) : Results.BadRequest(result);
});

app.Run();

