namespace RisedorApi.Shared.Common;

public class ErrorResponse
{
    public string Message { get; set; }
    public string? Detail { get; set; }
    public int StatusCode { get; set; }
    public IEnumerable<string>? Errors { get; set; }

    public ErrorResponse(string message)
    {
        Message = message;
    }
}
