using Reservation.Api.Enums;

namespace Reservation.Api.Models;

public class OperationResult<T>
{
    public OperationStatus Status { get; private init; }
    public T? Value { get; private init; }
    public string? Error { get; private init; }

    public bool IsSuccess => Status == OperationStatus.Success;

    public static OperationResult<T> Success(T value) => new() { Status = OperationStatus.Success, Value = value };
    public static OperationResult<T> ValidationFailed(string error) => new() { Status = OperationStatus.ValidationFailed, Error = error };
    public static OperationResult<T> NotFound(string error) => new() { Status = OperationStatus.NotFound, Error = error };
    public static OperationResult<T> Conflict(string error) => new() { Status = OperationStatus.Conflict, Error = error };
}
