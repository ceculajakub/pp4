using System.Net.Http.Headers;
using eCommerceMvc.Models.PayU;
using Newtonsoft.Json;

namespace eCommerceMvc.Services;

public class PayU : IPaymentGateway
{
    private readonly HttpClient _httpClient;
    private readonly PayUCredentials _credentials;

    public PayU(HttpClient httpClient, PayUCredentials credentials)
    {
        _httpClient = httpClient;
        _credentials = credentials;
    }

    public async Task<OrderCreateResponse> HandleAsync(OrderCreateRequest request)
    {
        var url = GetUrl("/api/v2_1/orders");

        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetTokenAsync());
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var jsonRequest = JsonConvert.SerializeObject(request);
        var httpContent = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(url, httpContent);

        response.EnsureSuccessStatusCode(); 

        var parsedResponse = new OrderCreateResponse
        {
            redirectUri = response.RequestMessage.RequestUri.ToString(),
            statusCode = response.StatusCode.ToString(),
        };
        return parsedResponse;
    }

    private async Task<string> GetTokenAsync()
    {
        var url = GetUrl("/pl/standard/user/oauth/authorize");
        var body = $"grant_type=client_credentials&client_id={_credentials.ClientId}&client_secret={_credentials.ClientSecret}";

        var httpContent = new StringContent(body, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");

        var response = await _httpClient.PostAsync(url, httpContent);

        response.EnsureSuccessStatusCode();

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var authorizationResponse = JsonConvert.DeserializeObject<AuthorizationResponse>(jsonResponse);

        return authorizationResponse.access_token;
    }

    private string GetUrl(string path)
    {
        return $"{_credentials.BaseUrl}{path}";
    }
}

public interface IPaymentGateway
{
    Task<OrderCreateResponse> HandleAsync(OrderCreateRequest request);
}