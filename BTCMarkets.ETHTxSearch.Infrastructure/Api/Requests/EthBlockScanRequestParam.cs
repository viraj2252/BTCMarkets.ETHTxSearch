using System;
namespace BTCMarkets.ETHTxSearch.Infrastructure.Api.Requests
{
    /// <summary>
    /// Request param related to Eth block scan
    /// </summary>
    public class EthBlockScanRequestParam : QueryBodyParams
    {
        public EthBlockScanRequestParam(string jsonRpc, string method, object[] parameters, int id) :
            base(jsonRpc, method, parameters, id)
        {

        }
    }
}
