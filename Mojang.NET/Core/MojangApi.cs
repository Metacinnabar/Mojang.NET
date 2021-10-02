using System;
using System.Net.Http;
using System.Threading.Tasks;
using Mojang.NET.Authentication;
using Mojang.NET.Common;
using Mojang.NET.Core;

namespace Mojang.NET
{
    public class MojangApi
    {
        public MojangApi(HttpClient httpClient)
        {
            
        }

        public async Task<ApiResponse<ServiceStatus>> CheckServiceStatus()
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<SaleStatistics>> GetSaleStatistics(SaleStatisticsKey saleKey)
        {
            throw new NotImplementedException();
        }

        //todo: unfuck
        public async Task<ApiResponse<Authentication.Authentication>> AuthenticateMojang(Credentials credentials)
        {
            throw new NotImplementedException();
        }
    }
}