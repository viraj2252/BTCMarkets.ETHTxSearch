using System;
using System.Threading.Tasks;
using BTCMarkets.ETHTxSearch.Core.Api;

namespace BTCMarkets.ETHTxSearch.Core.Interfaces
{
    /// <summary>
    /// Proxy to provide basig API operations
    /// </summary>
    public interface IApiProxy
    {
        Task<ApiResult<string>> Get(string action, IUrlParams parameters);
        Task<ApiResult<string>> Post(string action, IBodyParams parameters);
        Task<ApiResult<string>> Put(string action, IBodyParams parameters);
        Task<ApiResult<string>> Delete(string action, IUrlParams parameters);
    }
}
