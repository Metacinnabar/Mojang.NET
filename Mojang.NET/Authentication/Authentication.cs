using System.Threading.Tasks;
using Mojang.NET.Core;

namespace Mojang.NET.Authentication
{
    public class Authentication
    {
        public string AccessToken { get; private set; }

        //todo: unfuck
        public async Task<ApiResponse<Profile.Profile>> GetProfile()
        {
            throw new System.NotImplementedException();
        }

        public async Task<ApiResponse<Profile.Profile>> ChangeUsername(string newUsername)
        {
            throw new System.NotImplementedException();
        }
    }
}