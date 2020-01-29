using System;
namespace BTCMarkets.ETHTxSearch.Core.Interfaces
{
    /// <summary>
    /// POST request body data contract
    /// </summary>
    public interface IBodyParams
    {
        string ToJson();
    }
}
