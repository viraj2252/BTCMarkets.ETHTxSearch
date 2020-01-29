using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BTCMarkets.ETHTxSearch.Core.Interfaces;
using BTCMarkets.ETHTxSearch.Infrastructure.Api.Requests;
using BTCMarkets.ETHTxSearch.Web.Dtos;
using BTCMarkets.ETHTxSearch.Web.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BTCMarkets.ETHTxSearch.Web.Controllers
{

    public class BlockScanController : BaseController
    {
        private readonly IBlockDataService _blockDataService;
        private readonly ILogger<BlockScanController> _logger;
        private readonly IMapper _mapper;

        public BlockScanController(ILogger<BlockScanController> logger, IBlockDataService blockDataService, IMapper mapper)
        {
            _blockDataService = blockDataService;
            _logger = logger;
            _mapper = mapper;
        }
        // GET: api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] BlockScanRequest request)
        {
            int.TryParse(request.BlockId, out int blockId);

            var @params = new EthBlockScanRequestParam("2.0", "eth_getBlockByNumber", new object[] { $"0x{blockId.ToString("X")}", true }, 1);

            var result = await _blockDataService.SearchTransactionByBlockAndEthAddress(@params, request.EthAddress);

            if (result == null || !result.Any())
                return NotFound();

            return Ok(_mapper.Map<List<TransactionDTO>>(result));
        }
    }
}
