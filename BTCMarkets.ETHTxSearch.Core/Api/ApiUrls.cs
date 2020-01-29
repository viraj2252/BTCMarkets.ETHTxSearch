using System;
using BTCMarkets.ETHTxSearch.Core.Models;

namespace BTCMarkets.ETHTxSearch.Core.Api
{
    /// <summary>
    /// Action and the action type is defined to provide in the URL
    /// </summary>
    /// <typeparam name="TParams"></typeparam>
    /// <typeparam name="TResult"></typeparam>
    public class ApiActionAttributes<TParams, TResult>
    {
        public string Action { get; }
        public HttpMethods Method { get; }

        public ApiActionAttributes(string action, HttpMethods method)
        {
            Action = action;
            Method = method;
        }

    }
}
