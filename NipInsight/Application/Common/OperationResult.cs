namespace NipInsight.Application.Common;

public class OperationResult<T>
{
    public T Result { get; set; }

    public bool Success { get; set; }

    public string ErrorMessage { get; set; }
}