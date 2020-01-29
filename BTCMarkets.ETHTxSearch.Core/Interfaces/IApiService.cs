using System;
using System.Threading.Tasks;
using BTCMarkets.ETHTxSearch.Core.Api;

namespace BTCMarkets.ETHTxSearch.Core.Interfaces
{
    /// <summary>
    /// Define Execution of all API requests
    /// </summary>
    public interface IApiService
    {
        Task<ApiResult<TResult>> Execute<TParams, TResult>(ApiActionAttributes<TParams, TResult> apiAction, TParams @params);
    }
}
