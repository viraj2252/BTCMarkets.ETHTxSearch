using System;
using System.Threading.Tasks;
using BTCMarkets.ETHTxSearch.Core.Api;
using BTCMarkets.ETHTxSearch.Core.Interfaces;
using Newtonsoft.Json;

namespace BTCMarkets.ETHTxSearch.Infrastructure.Services
{
    public class InfuraApiService: IApiService
    {
        private readonly IApiProxy _apiProxy;

        public InfuraApiService(IApiProxy apiProxy)
        {
            _apiProxy = apiProxy;
        }

        public async Task<ApiResult<TResult>> Execute<TParams, TResult>(ApiActionAttributes<TParams, TResult> apiAction, TParams @params)
        {
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
