namespace Mojang.NET.Authentication
{
    public struct Authentication
    {
        public Authentication(string accessToken)
        {
            AccessToken = accessToken;
        }

        public string AccessToken { get; private set; }
    }
}