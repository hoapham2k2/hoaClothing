namespace identity_service.Models;

public class ServiceResponse<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; } // ? means nullable type
    public IEnumerable<string>? Messages { get; set; }
}
