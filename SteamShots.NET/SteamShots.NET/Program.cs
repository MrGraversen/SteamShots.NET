using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamShots.NET
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "SteamShots.NET";

            IEnumerable<string> steamUserIds = SteamUtil.GetUserProfiles();
            Console.WriteLine("{ " + string.Join(", ", steamUserIds.ToArray<string>()) + " }");
            foreach (string steamUserId in steamUserIds)
            {
                string steamUsername = SteamApiUtil.GetSteamUsername(FileUtil.ReadApiKey(), steamUserId);
                Console.WriteLine(steamUsername);
                Console.WriteLine();

                foreach (string gameId in SteamUtil.GetGamesWithScreenshots(steamUserId))
                {
                    Console.WriteLine(gameId);
                }
            }

            Console.ReadKey();
        }
    }
}
