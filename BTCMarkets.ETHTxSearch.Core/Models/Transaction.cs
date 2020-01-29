using System;
using Newtonsoft.Json;

namespace BTCMarkets.ETHTxSearch.Core.Models
{
    public class Transaction
    {
        [JsonProperty("blockHash")]
        public string BlockHash { get; set; }

        [JsonProperty("blockNumber")]
        public string BlockNumber { get; set; }

        [JsonProperty("gas")]
        public string Gas { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }
}
