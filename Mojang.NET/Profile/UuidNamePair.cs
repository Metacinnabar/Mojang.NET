namespace Mojang.NET.Profile
{
    public struct UuidNamePair
    {
        public UuidNamePair(string username, string uuid)
        {
            Username = username;
            Uuid = uuid;
        }

        public string Username { get; set; }
        public string Uuid { get; set; }
    }
}