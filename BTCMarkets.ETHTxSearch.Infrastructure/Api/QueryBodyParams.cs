using System;
using BTCMarkets.ETHTxSearch.Core.Interfaces;
using Newtonsoft.Json;

namespace BTCMarkets.ETHTxSearch.Infrastructure.Api
{
    /// <summary>
    /// All POSt data required to do block scan
    /// </summary>
    public class QueryBodyParams: IBodyParams
    {
        [JsonProperty("jsonrpc")]
        public string JsonRpc { get; set; }

        [JsonProperty("method")]
        public string Method { get; set; }

        [JsonProperty("params")]
        public object[] Params { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        public QueryBodyParams(string jsonRpc, string method, object[] parameters, int id)
        {
            JsonRpc = jsonRpc;
            Method = method;
            Params = parameters;
            Id = id;
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
