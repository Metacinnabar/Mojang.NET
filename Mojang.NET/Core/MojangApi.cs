using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Mojang.NET.Authentication;
using Mojang.NET.Common;
using Mojang.NET.Core;
using Mojang.NET.Profile;

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

        public async Task<ApiResponse<UuidNamePair>> UsernameToUuid(string username)
        {
            // GET https://api.mojang.com/users/profiles/minecraft/<username>
            
            throw new NotImplementedException();
        }
        
        public async Task<ApiResponse<List<UuidNamePair>>> UsernamesToUuid(List<string> usernames)
        {
            // POST https://api.mojang.com/profiles/minecraft
            // payload example:
            /*[
                "foo",
                "bar",
                "nonExistingPlayer"
            ]*/

            if (usernames.Count >= 10)
            {
                //throw new IllegalArgumentException("Not more that 10 profile name per call is allowed.")
            }
            
            foreach (var username in usernames)
            {

            }
            
            throw new NotImplementedException();
        }

        public async Task<ApiResponse<NameHistory[]>> GetNameHistory(string uuid)
        {
            throw new NotImplementedException();
        }
    }
}