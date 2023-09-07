namespace product_service.Model;

public class ServiceResponse<T> where T : class
{
    public T Data { get; set; }
    public bool Success { get; set; } = true;
    public IEnumerable<string> Messages { get; set; } = null;
}