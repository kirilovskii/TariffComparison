using System.Net;
using System.Text.Json;

namespace TariffComparison.FunctionalTests.Drivers
{
    public class TariffComparisonDriver
    {
        private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        private readonly HttpClient _httpClient;

        public TariffComparisonDriver(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<(HttpStatusCode, ProductCosts[]?)> GetComparison(int consumption)
        {
            HttpResponseMessage response = await _httpClient
                .GetAsync($"Comparison?consumption={consumption}");

            ProductCosts[]? costs = response.StatusCode == HttpStatusCode.OK
                ? await JsonSerializer.DeserializeAsync<ProductCosts[]>(response.Content.ReadAsStream(), _jsonOptions)
                : null;

            return (response.StatusCode, costs);
        }
    }
}
