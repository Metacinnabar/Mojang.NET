using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using dotenv.net;
using Mojang.NET.Authentication;
using Mojang.NET.Common;
using Mojang.NET.Core;
using Mojang.NET.Profile;
using Mojang.NET.Utils;

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

            var mcSoldStatsResponse = await mojangApi.GetSaleStatistics(SaleStatisticsKey.MinecraftSold);
            if (mcSoldStatsResponse.Successful)
            {
                var mcSoldStats = mcSoldStatsResponse.Response;
                Console.WriteLine(mcSoldStats.TotalSold);
                Console.WriteLine(mcSoldStats.SoldLast24h);
                Console.WriteLine(mcSoldStats.AverageSalesPerSecond);
            }

            var metacinnabarUuidResponse = await mojangApi.UsernameToUuid("metacinnabar");
            if (metacinnabarUuidResponse.Successful)
            {
                var metacinnabarUuid = metacinnabarUuidResponse.Response;
                Console.WriteLine($"UUID for username \"metacinnabar\": ${metacinnabarUuid}");
            }

            var usernames = new List<string>();
            usernames.Add("Uranometry");
            usernames.Add("metacinnabar");
            
            var random = new Random();
            if (random.Next(0, 1) == 1)
                usernames.Add("AnimeToggled");

            var usernameToUuidResponses = await mojangApi.UsernamesToUuid(usernames);
            if (usernameToUuidResponses.Successful)
            {
                foreach (var usernameToUuid in usernameToUuidResponses.Response)
                {
                    Console.WriteLine($"UUID for username '${usernameToUuid.Username}': '${usernameToUuid.Uuid}'");
                    
                    var nameHistoriesResponse = await mojangApi.GetNameHistory(usernameToUuid.Uuid);
                    if (nameHistoriesResponse.Successful)
                    {
                        var nameHistories = nameHistoriesResponse.Response;
                        for (var index = 0; index <= nameHistories.Length; index++)
                        {
                            var nameHistory = nameHistories[index];
                            if (nameHistory.IsCurrentUsername())
                            {
                                Console.WriteLine("Current username: " + nameHistory.Name);
                            }
                            else if (nameHistory.ChangedToAt != null)
                            {
                                var dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(nameHistory.ChangedToAt.Value);
                                Console.WriteLine($"Previous username (${OrdinalUtils.AddOrdinal(index)} most recent username): " +
                                                  $"${nameHistory.Name} changed at: " +
                                                  $"${dateTimeOffset.Date}");
                            }
                        }
                    }
                }
            }

            var envVars = DotEnv.Read();

            Console.WriteLine("Use main account? [y]");
            var useMainAccount = Console.ReadLine() == "y";

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

                    Console.WriteLine("Profile uuid: " + profile.Uuid);

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