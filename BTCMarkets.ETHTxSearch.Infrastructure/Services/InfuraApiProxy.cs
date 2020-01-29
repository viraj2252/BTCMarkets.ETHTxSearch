using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BTCMarkets.ETHTxSearch.Core.Api;
using BTCMarkets.ETHTxSearch.Core.Interfaces;
using BTCMarkets.ETHTxSearch.Infrastructure.Api;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BTCMarkets.ETHTxSearch.Infrastructure.Services
{
    /// <summary>
    /// Proxy to make API calls to Infura API
    /// ATM only POST is implemented as per the requirement
    /// </summary>
    public class InfuraApiProxy: IApiProxy
    {
        private readonly HttpClient _httpClient;
        EthEnvironment _environment;
        private readonly string _projectId;
        private readonly ILogger _logger;

        public InfuraApiProxy(ILogger logger, EthEnvironment environment, string projectId)
        {
            _environment = environment;
            _projectId = projectId;
            _logger = logger;
            _httpClient = new HttpClient { BaseAddress = new Uri($"https://{InfuraEnvironments.Values[_environment]}/v3/{_projectId}") };

        }

        public Task<ApiResult<string>> Delete(string action, IUrlParams parameters)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<string>> Get(string action, IUrlParams parameters)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResult<string>> Post(string action, IBodyParams parameters)
        {
            return SendAndGetResponseAsync(HttpMethod.Post, action, parameters);
        }

        public Task<ApiResult<string>> Put(string action, IBodyParams parameters)
        {
            throw new NotImplementedException();
        }

        private Task<ApiResult<string>> SendAndGetResponseAsync(HttpMethod method, string action, IBodyParams parameters)
        {
            var content = parameters?.ToJson() ?? string.Empty;
            _logger.LogInformation($"{action} sending content:{content}");
            var request = new HttpRequestMessage(method, " ")
            {
                Content = new StringContent(content, Encoding.UTF8, "application/json")
            };

            CorrectUri(request);

            return SendAndGetResponseAsync(request, content);
        }

        private static void CorrectUri(HttpRequestMessage request)
        {
            request.RequestUri = new Uri(request.RequestUri.OriginalString, UriKind.Relative);
        }

        private async Task<ApiResult<string>> SendAndGetResponseAsync(HttpRequestMessage request, string @params = null)
        {

            _logger.LogInformation($"{request.Method} {request.RequestUri}");

            var response = await _httpClient.SendAsync(request);
            var responseString = await response.Content.ReadAsStringAsync();

            _logger.LogInformation($"{request.Method} {request.RequestUri.PathAndQuery} {(response.IsSuccessStatusCode ? "resp" : "errorResp")}:{responseString}");

            if (!response.IsSuccessStatusCode)
            {
                try
                {
                    throw new ApiException((int)response.StatusCode, "Error occurred in Infura API");
                }
                catch (JsonReaderException)
                {
                    throw new ApiException((int)response.StatusCode, responseString);
                }
            }

            return new ApiResult<string>(responseString);
        }
    }
}
