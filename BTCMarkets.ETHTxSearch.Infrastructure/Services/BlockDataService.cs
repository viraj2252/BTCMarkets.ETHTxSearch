using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BTCMarkets.ETHTxSearch.Core.Interfaces;
using BTCMarkets.ETHTxSearch.Core.Models;
using BTCMarkets.ETHTxSearch.Infrastructure.Api;
using BTCMarkets.ETHTxSearch.Infrastructure.Api.Requests;
using Microsoft.Extensions.Logging;

namespace BTCMarkets.ETHTxSearch.Infrastructure.Services
{
    /// <summary>
    /// Implementation of block data service as our API will access this service
    /// </summary>
    public class BlockDataService: IBlockDataService
    {
        private readonly IApiService _apiService;
        private readonly ILogger<BlockDataService> _logger;

        public BlockDataService(ILogger<BlockDataService> logger, IApiService apiService)
        {
            _apiService = apiService;
            _logger = logger;
        }

        public async Task<ICollection<Transaction>> SearchTransactionByBlockAndEthAddress(IBodyParams @bodyParams, string ethAddress)
        {
            //int.TryParse(blockIdStr, out int blockId);

            //var @params = new EthBlockScanRequestParam("2.0", "eth_getBlockByNumber", new object[] { $"0x{blockId.ToString("X")}", true }, 1);
            var resultTask = await _apiService.Execute(ApiUrls.GetTransactions, @bodyParams as EthBlockScanRequestParam);
            var result = resultTask.Result;

            if (result == null || result.Result == null)
                throw new KeyNotFoundException("No result found for that block");

            return result.Result.Transactions.Where(t => t.From == ethAddress).ToList();

        }
    }
}
