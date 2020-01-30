using System;
namespace BTCMarkets.ETHTxSearch.Infrastructure.Api
{
    public class ApiOptions
    {
        public EthEnvironment ApiEnvironment { get; set; }
        public string ProjectId { get; set; }
        public string Version { get; set; }
    }
}
