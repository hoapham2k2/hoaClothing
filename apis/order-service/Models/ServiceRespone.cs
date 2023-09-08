namespace order_service.Migrations;

public class ServiceRespone<T> where T : class
{
    public bool Success { get; set; } = true;
    public T Data { get; set; }
    public IEnumerable<string> Errors { get; set; } = null!;
}