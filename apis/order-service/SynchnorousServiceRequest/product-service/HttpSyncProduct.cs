using Newtonsoft.Json;
using order_service.Migrations;

namespace order_service.SynchnorousServiceRequest.product_service;

public class HttpSyncProduct : IHttpSyncProduct
{
    
    private readonly ILogger<HttpSyncProduct> _logger;
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;
    
    public HttpSyncProduct(ILogger<HttpSyncProduct> logger, IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _configuration = configuration;
        _httpClient = httpClientFactory.CreateClient("ProductService");
    }

    

    public async Task<string> GetTest()
    {
        var response = await _httpClient.GetAsync("/api/Private/test");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            return content;
        }
        else
        {
            _logger.LogError("Error when calling product service");
            return null;
        }
    }

    public async Task<bool> CheckProductExist(int productId)
    {
        var response = await _httpClient.GetAsync($"/api/Private/CheckProductExist/{productId}");
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<bool>(content);
            return result;
        }
        else
        {
            _logger.LogError("Error when calling product service");
            return false;
        }
    }
}