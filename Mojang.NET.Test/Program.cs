using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mojang.NET.Test
{
    public class Program
    {
        private static async Task Main(string[] args)
        {
            var httpClient = new HttpClient();
            var mojangApi = new MojangApi(httpClient);
            var statusResponse = await mojangApi.CheckServiceStatus();

            if (statusResponse.Successful)
            {
                var status = statusResponse.Response;
                Console.WriteLine(status.Textures);

                switch (status)
                {
                    case {Mojang: ServiceAvailability.Available}:
                        Console.WriteLine("mojang.com is available.");
                        break;
                    case {Minecraft: ServiceAvailability.Issues}:
                        Console.WriteLine("minecraft.com is having some issues.");
                        break;
                    case {Api: ServiceAvailability.Unavailable}:
                        Console.WriteLine("api.mojang.com is unavailable.");
                        break;
                }
            }

            var mcSoldStatsResponse = await mojangApi.GetSaleStatistics(SaleStatisticKey.MinecraftSold);

            if (mcSoldStatsResponse.Successful)
            {
                var mcSoldStats = mcSoldStatsResponse.Response;
                Console.WriteLine(mcSoldStats.TotalSold);
                Console.WriteLine(mcSoldStats.SoldLast24h);
                Console.WriteLine(mcSoldStats.AverageSalesPerSecond);
            }

            var envVars = DotEnv.Read();

            Console.WriteLine("Use main account? [yes / y]");
            var useMainAccount = Console.ReadLine() == "y" || Console.ReadLine() == "yes";

            var mainSecurityQuestions = new SecurityQuestions(envVars["MAIN_SECURITY_1"], envVars["MAIN_SECURITY_2"],
                envVars["MAIN_SECURITY_3"]);

            var credentials = useMainAccount
                ? new Credentials(envVars["MAIN_USERNAME"], envVars["MAIN_PASSWORD"], mainSecurityQuestions)
                : new Credentials(envVars["ALT_USERNAME"], envVars["ALT_PASSWORD"]);
            var authResponse = await mojangApi.AuthenticateMojang(credentials);

            if (authResponse.Successful)
            {
                var auth = authResponse.Response;
                var profileResponse = await auth.GetProfile();

                if (profileResponse.Successful)
                {
                    var profile = profileResponse.Response;

                    Console.WriteLine(profile.Uuid.Expanded);
                    Console.WriteLine(profile.Uuid.Contracted);

                    // if name change successful, update profile var with new username
                    var nameChangeResponse = await auth.ChangeUsername("new_username");

                    if (nameChangeResponse.Successful)
                    {
                        Console.WriteLine("Successfully changed username!");
                        profile = nameChangeResponse.Response;
                    }

                    if (profile.Capes.Length == 0)
                    {
                        Console.WriteLine("No capes found!");
                    }
                }
            }
        }
    }
}