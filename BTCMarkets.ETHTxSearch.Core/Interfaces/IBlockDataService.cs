using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BTCMarkets.ETHTxSearch.Core.Models;

namespace BTCMarkets.ETHTxSearch.Core.Interfaces
{
    /// <summary>
    /// Contract of transaction search
    /// </summary>
    public interface IBlockDataService
    {
        Task<ICollection<Transaction>> SearchTransactionByBlockAndEthAddress(IBodyParams @bodyParams, string ethAddress);
    }
}
