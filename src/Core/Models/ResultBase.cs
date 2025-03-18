namespace Models;
public class Result<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public string? Error { get; }

    public Result(T value) => (IsSuccess, Value) = (true, value);
    public Result(string error) => (IsSuccess, Error) = (false, error);
}
