using System;
using Newtonsoft.Json;

namespace BTCMarkets.ETHTxSearch.Core.Api
{
    public class ApiException: Exception
    {
        public int StatusCode { get; }

        public ApiException(int statusCode, string errorMessage)
            : base($"{statusCode}: {errorMessage}")
        {
            StatusCode = statusCode;
        }
    }
}
