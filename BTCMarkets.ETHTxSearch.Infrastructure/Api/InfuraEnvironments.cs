using System;
using System.Collections.Generic;

namespace BTCMarkets.ETHTxSearch.Infrastructure.Api
{
    public static class Environments
    {
        //No need to add this to config as we know this is the endpoint we are going to use to scan for transactions
        public static readonly IDictionary<EthEnvironment, string> Values = new Dictionary<EthEnvironment, string>
        {
            { EthEnvironment.Mainnet, "mainnet.infura.io"},
            { EthEnvironment.Ropsten, "ropsten.infura.io"},
            { EthEnvironment.Rinkeby, "rinkeby.infura.io"},
            { EthEnvironment.Kovan, "kovan.infura.io"},
            { EthEnvironment.Gorli, "goerli.infura.io"}
        };
    }

    public enum EthEnvironment
    {
        Mainnet = 1,
        Ropsten = 2,
        Rinkeby = 3,
        Kovan = 4,
        Gorli = 5
    }
}
