using Reservation.Api.Interfaces;

namespace Reservation.Api.Invokers;

public class CommandInvoker : ICommandInvoker
{
    public Task Invoke(ICommand command) => command.Execute();
}
