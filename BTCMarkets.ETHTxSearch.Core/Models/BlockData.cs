using System;
using Newtonsoft.Json;

namespace BTCMarkets.ETHTxSearch.Core.Models
{
    public class BlockData
    {
        [JsonProperty("difficulty")]
        public string Difficulty { get; set; }

        [JsonProperty("transactions")]
        public Transaction[] Transactions { get; set; }
    }
}
