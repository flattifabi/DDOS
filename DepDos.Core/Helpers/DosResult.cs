namespace DepDos.Core.Helpers;

public class DosResult<T>
{
    public T? ResultData { get; set; } = default;
    public string? ErrorText { get; set; } = "NoError";
    public int? ErrorCode { get; set; } = 0;
}