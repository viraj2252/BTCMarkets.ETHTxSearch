using System;
namespace BTCMarkets.ETHTxSearch.Core.Api
{
    public class ApiResult<TResult>
    {
        private object result;

        /// <summary>
        /// The result
        /// </summary>
        public TResult Result { get; }


        public ApiResult(TResult result)
        {
            this.Result = result;
        }

        public ApiResult(object result)
        {
            this.result = result;
        }

        public ApiResult<T> ToResultType<T>(T result)
        {
            return new ApiResult<T>(result);
        }
    }
}
