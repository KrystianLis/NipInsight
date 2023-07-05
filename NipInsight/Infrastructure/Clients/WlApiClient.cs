using Newtonsoft.Json;
using NipInsight.Domain.Entities;
using NipInsight.Domain.Interfaces.Clients;
using Exception = NipInsight.Domain.Entities.Exception;

namespace NipInsight.Infrastructure.Clients;

public class WlApiClient : IWlApiClient
{
    private readonly HttpClient _httpClient;

    public WlApiClient(HttpClient httpClient)
        => _httpClient = httpClient;

    public async Task<EntityResponse> GetCompanyDataByNipAsync(string nip, DateTime dateTime)
    {
        var response = await _httpClient.GetAsync($"/api/search/nip/{nip}?date={dateTime:yyyy-MM-dd}");
        var data = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            return new EntityResponse { Exception = JsonConvert.DeserializeObject<Exception>(data)! };
        }

        var result = JsonConvert.DeserializeObject<EntityResponse>(data);

        return result!;
    }
}