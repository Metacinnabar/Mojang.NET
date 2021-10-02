namespace Mojang.NET.Common
{
    public struct ServiceStatus
    {
        public ServiceAvailability Minecraft { get; set; }
        public ServiceAvailability Mojang { get; set; }
        public ServiceAvailability Accounts { get; set; }
        public ServiceAvailability Sessions { get;  set; }
        public ServiceAvailability AuthenticationServer { get; set; }
        public ServiceAvailability SessionsServer { get; set; }
        public ServiceAvailability Textures { get; set; }
        public ServiceAvailability Api { get; set; }
    }
}