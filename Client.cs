using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StanzaApi.X12Parser
{
    public class X12ParserClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _baseUrl;
        private readonly string _validateEndpoint;
        private readonly string _parseEndpoint;
        public string ToolUrl { get; } = "https://stanzaapi.com/tools/x12-parser";

        public X12ParserClient(string apiKey = null, string baseUrl = null, string tier = "sandbox", HttpClient httpClient = null)
        {
            _apiKey = apiKey ?? Environment.GetEnvironmentVariable("STANZA_API_KEY") ?? Environment.GetEnvironmentVariable("API_KEY") ?? "";
            _baseUrl = (baseUrl ?? (tier == "enterprise" ? "https://secure.api.stanzaapi.com" : "https://api.stanzaapi.com/x12-parser")).TrimEnd('/');
            _validateEndpoint = tier == "enterprise" ? "/v1/x12/validate" : "/api/v1/x12/validate";
            _parseEndpoint = tier == "enterprise" ? "/v1/x12/parse" : "/api/v1/x12/parse";
            _httpClient = httpClient ?? new HttpClient { Timeout = TimeSpan.FromSeconds(15) };
        }

        private async Task<string> SendRequestAsync(string endpoint, HttpMethod method, string jsonBody = null)
        {
            var url = $"{_baseUrl}/{endpoint.TrimStart('/')}";
            var request = new HttpRequestMessage(method, url);
            request.Headers.Add("Accept", "application/json");

            if (!string.IsNullOrEmpty(_apiKey))
            {
                request.Headers.Add("x-api-key", _apiKey);
                request.Headers.Add("Authorization", $"Bearer {_apiKey}");
            }

            if (jsonBody != null)
            {
                request.Content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
            }

            var response = await _httpClient.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }

        public Task<string> GetHealthAsync()
        {
            return SendRequestAsync("/health", HttpMethod.Get);
        }

        public Task<string> ValidateAsync(string jsonPayload)
        {
            return SendRequestAsync(_validateEndpoint, HttpMethod.Post, jsonPayload);
        }

        public Task<string> ParseAsync(string jsonPayload)
        {
            return SendRequestAsync(_parseEndpoint, HttpMethod.Post, jsonPayload);
        }
    }
}
