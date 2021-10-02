namespace Mojang.NET.Authentication
{
    public struct Credentials
    {
        public Credentials(string username, string password)
        {
            Username = username;
            Password = password;
            SecurityQuestions = default;
        }
        
        public Credentials(string username, string password, SecurityQuestions securityQuestions)
        {
            Username = username;
            Password = password;
            SecurityQuestions = securityQuestions;
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public SecurityQuestions SecurityQuestions { get; set; }
    }
}