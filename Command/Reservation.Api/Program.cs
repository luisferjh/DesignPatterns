using Microsoft.EntityFrameworkCore;
using Reservation.Api.Commands;
using Reservation.Api.Data;
using Reservation.Api.Enums;
using Reservation.Api.Interfaces;
using Reservation.Api.Invokers;
using Reservation.Api.Models;
using Reservation.Api.Repositories;
using Reservation.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ReservationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Reservations")));
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<ICommandInvoker, CommandInvoker>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ReservationDbContext>();
    db.Database.Migrate();

    if (!db.Resources.Any())
    {
        db.Resources.AddRange(
            new Resource { Id = Guid.Parse("11111111-1111-1111-1111-111111111111"), Name = "Resource A" },
            new Resource { Id = Guid.Parse("22222222-2222-2222-2222-222222222222"), Name = "Resource B" },
            new Resource { Id = Guid.Parse("33333333-3333-3333-3333-333333333333"), Name = "Resource C" });
        db.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPost("/reservations", async (CreateReservationRequest request, IReservationService service, ICommandInvoker invoker) =>
{
    var command = new CreateReservationCommand(request, service);
    await invoker.Invoke(command);

    return ToHttpResult(command.Result!, reservation => Results.Created($"/reservations/{reservation.Id}", reservation));
})
.WithName("CreateReservation");

app.MapPost("/reservations/{id:guid}/cancel", async (Guid id, IReservationService service, ICommandInvoker invoker) =>
{
    var command = new CancelReservationCommand(id, service);
    await invoker.Invoke(command);

    return ToHttpResult(command.Result!, Results.Ok);
})
.WithName("CancelReservation");

app.MapPost("/reservations/{id:guid}/reschedule", async (Guid id, RescheduleReservationRequest request, IReservationService service, ICommandInvoker invoker) =>
{
    var command = new RescheduleReservationCommand(id, request, service);
    await invoker.Invoke(command);

    return ToHttpResult(command.Result!, Results.Ok);
})
.WithName("RescheduleReservation");

app.MapPost("/reservations/{id:guid}/confirm", async (Guid id, IReservationService service, ICommandInvoker invoker) =>
{
    var command = new ConfirmReservationCommand(id, service);
    await invoker.Invoke(command);

    return ToHttpResult(command.Result!, Results.Ok);
})
.WithName("ConfirmReservation");

app.MapGet("/reservations", async (IReservationService service) =>
    Results.Ok(await service.ListAsync()))
.WithName("ListReservations");

app.Run();

static IResult ToHttpResult(OperationResult<ReservationResponse> result, Func<ReservationResponse, IResult> onSuccess) =>
    result.Status switch
    {
        OperationStatus.Success => onSuccess(result.Value!),
        OperationStatus.ValidationFailed => Results.ValidationProblem(
            new Dictionary<string, string[]> { ["request"] = [result.Error!] }),
        OperationStatus.NotFound => Results.Problem(result.Error, statusCode: StatusCodes.Status404NotFound),
        OperationStatus.Conflict => Results.Problem(result.Error, statusCode: StatusCodes.Status409Conflict),
        _ => Results.Problem(statusCode: StatusCodes.Status500InternalServerError)
    };

