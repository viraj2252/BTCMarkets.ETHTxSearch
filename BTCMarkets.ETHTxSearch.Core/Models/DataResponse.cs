using System;
using Newtonsoft.Json;

namespace BTCMarkets.ETHTxSearch.Core.Models
{
    public class DataResponse<T>
    {
        [JsonProperty("jsonrpc")]
        public string Jsonrpc { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("result")]
        public T Result { get; set; }
    }
}
