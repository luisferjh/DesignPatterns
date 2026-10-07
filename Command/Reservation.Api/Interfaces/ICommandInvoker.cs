namespace Reservation.Api.Interfaces;

public interface ICommandInvoker
{
    Task Invoke(ICommand command);
}
