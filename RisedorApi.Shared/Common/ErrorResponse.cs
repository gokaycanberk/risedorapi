namespace RisedorApi.Shared.Common;

public class ErrorResponse
{
    public string Message { get; set; }
    public string? Detail { get; set; }
    public IEnumerable<string>? Errors { get; set; }

    public ErrorResponse(string message, string? detail = null, IEnumerable<string>? errors = null)
    {
        Message = message;
        Detail = detail;
        Errors = errors;
    }
}
