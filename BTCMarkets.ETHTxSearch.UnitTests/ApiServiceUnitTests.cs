using System;
using System.Threading.Tasks;
using BTCMarkets.ETHTxSearch.Core.Api;
using BTCMarkets.ETHTxSearch.Core.Interfaces;
using BTCMarkets.ETHTxSearch.Core.Models;
using BTCMarkets.ETHTxSearch.Infrastructure.Api;
using BTCMarkets.ETHTxSearch.Infrastructure.Api.Requests;
using BTCMarkets.ETHTxSearch.Core.Api;
using Moq;
using Xunit;
using Microsoft.Extensions.Logging;
using BTCMarkets.ETHTxSearch.Infrastructure.Services;
using System.Collections.Generic;

namespace BTCMarkets.ETHTxSearch.UnitTests
{
    public class ApiServiceUnitTest
    {
        private Mock<IApiService> _apiService;
        private Mock<ILogger<BlockDataService>> _logger;
        private BlockDataService _dataService;
        private Mock<IBodyParams> _bodyParam;

        public ApiServiceUnitTest()
        {
            _apiService = new Mock<IApiService>();
            _logger = new Mock<ILogger<BlockDataService>>();
            _bodyParam = new Mock<IBodyParams>();
        }


        [Fact]
        public async Task DataService_SearchTransactionByBlockAndEthAddress_should_fire_KeyNotFoundException()
        {

            ApiResult<DataResponse<BlockData>> ss = new Core.Api.ApiResult<DataResponse<BlockData>>(new DataResponse<BlockData>());
            var @params = new EthBlockScanRequestParam("2.0", "eth_getBlockByNumber", new object[] { $"0x{1.ToString("X")}", true }, 1);

             _apiService.Setup(p => p.Execute(ApiUrls.GetTransactions, @params)).Returns(() => Task.FromResult(ss));
            
            _dataService = new BlockDataService(_logger.Object, _apiService.Object);
            await Assert.ThrowsAsync<KeyNotFoundException>(async () => await _dataService.SearchTransactionByBlockAndEthAddress(@params, ""));
            
        }

        [Fact]
        public async Task DataService_SearchTransactionByBlockAndEthAddress_should_return_0_tx()
        {
            BlockData blockData = new BlockData
            {
                Difficulty = "1",
                Transactions = new Transaction[]
                {
                    new Transaction { From = "0x29d7d1dd5b6f9c864d9db560d72a247c178ae86b" },
                    new Transaction { From = "0x29d7d1dd5b6f9c864d9db560d72a247c178ae86c"}
                }
            };
            var dataResp = new DataResponse<BlockData>
            {
                Result = blockData
            };

            ApiResult<DataResponse<BlockData>> ss = new Core.Api.ApiResult<DataResponse<BlockData>>(dataResp);
            var @params = new EthBlockScanRequestParam("2.0", "eth_getBlockByNumber", new object[] { $"0x{1.ToString("X")}", true }, 1);

            _apiService.Setup(p => p.Execute(ApiUrls.GetTransactions, @params)).Returns(() => Task.FromResult(ss));

            _dataService = new BlockDataService(_logger.Object, _apiService.Object);
            var result = await _dataService.SearchTransactionByBlockAndEthAddress(@params, "0x29d7d1dd5b6f9c864d9db560d72a247c178ae86d");

            Assert.NotNull(result);
            Assert.Equal(0, result.Count);
        }

        [Fact]
        public async Task DataService_SearchTransactionByBlockAndEthAddress_should_return_one_tx()
        {
            BlockData blockData = new BlockData
            {
                Difficulty = "1",
                Transactions = new Transaction[]
                {
                    new Transaction { From = "0x29d7d1dd5b6f9c864d9db560d72a247c178ae86b" },
                    new Transaction { From = "0x29d7d1dd5b6f9c864d9db560d72a247c178ae86c"}
                }
            };
            var dataResp = new DataResponse<BlockData>
            {
                Result = blockData
            };

            ApiResult<DataResponse<BlockData>> ss = new Core.Api.ApiResult<DataResponse<BlockData>>(dataResp);
            var @params = new EthBlockScanRequestParam("2.0", "eth_getBlockByNumber", new object[] { $"0x{1.ToString("X")}", true }, 1);

            _apiService.Setup(p => p.Execute(ApiUrls.GetTransactions, @params)).Returns(() => Task.FromResult(ss));

            _dataService = new BlockDataService(_logger.Object, _apiService.Object);
            var result = await _dataService.SearchTransactionByBlockAndEthAddress(@params, "0x29d7d1dd5b6f9c864d9db560d72a247c178ae86b");

            Assert.NotNull(result);
            Assert.Equal(1, result.Count);
        }
    }

}
