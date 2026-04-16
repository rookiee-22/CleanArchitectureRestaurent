namespace WebApi.Middleware;

public class ApiErrorResponse
{
    public int StatusCode { get; set; }
    public bool Success => false;
    public string Message { get; set; } = string.Empty;
    public string? Details { get; set; }  // Only shown in Development
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
