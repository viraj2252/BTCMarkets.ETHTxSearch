using System;
namespace BTCMarkets.ETHTxSearch.Web.Requests
{
    public class BlockScanRequest
    {
		public string BlockId { get; set; }
        public string EthAddress { get; set; }
    }
}
