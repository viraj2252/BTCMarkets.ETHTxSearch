using System;
using System.Threading.Tasks;
using BTCMarkets.ETHTxSearch.Core.Api;
using BTCMarkets.ETHTxSearch.Core.Interfaces;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BTCMarkets.ETHTxSearch.Infrastructure.Services
{
    public class InfuraApiService: IApiService
    {
        private readonly IApiProxy _apiProxy;
        private readonly ILogger<InfuraApiService> _logger;

        public InfuraApiService(IApiProxy apiProxy, ILogger<InfuraApiService> logger)
        {
            _apiProxy = apiProxy;
            _logger = logger;
        }

        public async Task<ApiResult<TResult>> Execute<TParams, TResult>(ApiActionAttributes<TParams, TResult> apiAction, TParams @params)
        {
            _logger.LogInformation("Method: Execute");
            switch (apiAction.Method)
            {
                case Core.Models.HttpMethods.POST:
                    var postQueryParams = @params as IBodyParams;
                    var serializedResult = await _apiProxy.Post(apiAction.Action, postQueryParams);
                    var deserializedResult = JsonConvert.DeserializeObject<TResult>(serializedResult.Result);
                    return serializedResult.ToResultType<TResult>(deserializedResult);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
