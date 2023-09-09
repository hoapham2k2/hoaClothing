using Newtonsoft.Json;

namespace order_service.SynchnorousServiceRequest.auth_service;

public class HttpSyncAuth : IHttpSyncAuth
{
    private readonly ILogger<HttpSyncAuth> _logger;
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;
    
    public HttpSyncAuth(ILogger<HttpSyncAuth> logger, IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        _configuration = configuration;
        _httpClient = httpClientFactory.CreateClient("AuthService");
    }


    public async Task<bool> CheckUserExist(int userId)
    {
        var response = await _httpClient.GetAsync($"/api/Private/CheckUserExist/{userId}");
        
        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<bool>(content);
            return result;
        }
        else
        {
            _logger.LogError("Error when calling auth service");
            return false;
        }
    }
}