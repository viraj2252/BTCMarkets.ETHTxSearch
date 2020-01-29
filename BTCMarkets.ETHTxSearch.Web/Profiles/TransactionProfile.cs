using AutoMapper;
using BTCMarkets.ETHTxSearch.Core.Models;
using BTCMarkets.ETHTxSearch.Web.Dtos;

namespace BTCMarkets.ETHTxSearch.Web.Profiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionDTO>();
        }
    }
}