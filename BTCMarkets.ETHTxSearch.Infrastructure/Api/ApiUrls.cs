using System;
using System.Collections.Generic;
using BTCMarkets.ETHTxSearch.Core.Api;
using BTCMarkets.ETHTxSearch.Core.Models;
using BTCMarkets.ETHTxSearch.Infrastructure.Api.Requests;

namespace BTCMarkets.ETHTxSearch.Infrastructure.Api
{
    /// <summary>
    /// All URLs related to external API calls are defined here along with parameters, return types
    /// </summary>
    public static partial class ApiUrls
    {
        public static ApiActionAttributes<EthBlockScanRequestParam, DataResponse<BlockData>> GetTransactions = new ApiActionAttributes<EthBlockScanRequestParam, DataResponse<BlockData>>("transactions", HttpMethods.POST);
    }
}
