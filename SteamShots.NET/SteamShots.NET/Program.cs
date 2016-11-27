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

            PrintWelcome();
            if (!FileUtil.ApiKeyFileExists())
            {
                string apiKeyFileName = FileUtil.CreateApiKeyFile();

                PrintHiglightError("You must supply a valid Steam Web API key!");
                Console.WriteLine(string.Format("Please insert your API key in the {0} file", apiKeyFileName));
                WaitForInput();
                Environment.Exit(0);
            }

            FileUtil.CreateSteamShotsDirectory();

            string steamInstallPath = RegistryUtil.GetSteamInstallPath();

            if (steamInstallPath == null)
            {
                PrintHiglightError("Steam does not appear to be installed!");
                WaitForInput();
                Environment.Exit(0);
            }

            PrintHiglightInfo("Discovered Steam: ", steamInstallPath);

            IEnumerable<string> steamUserIds = SteamUtil.GetUserProfiles();

            if (steamUserIds == null || !steamUserIds.Any())
            {
                PrintHiglightError("No Steam screenshots discovered!");
                WaitForInput();
                Environment.Exit(0);
            }

            PrintHiglightInfo("Discovered users with IDs: ", "{ " + string.Join(", ", steamUserIds.ToArray<string>()) + " }");

            foreach (string steamUserId in steamUserIds)
            {
                string steamUsername = SteamApiUtil.GetSteamUsername(FileUtil.ReadApiKey(), steamUserId);

                if (steamUsername != null)
                {
                    PrintHiglightSuccess(string.Format("Resolved Steam user ID {0}: ", steamUserId), steamUsername);

                    IEnumerable<string> gameIds = SteamUtil.GetGamesWithScreenshots(steamUserId);

                    if (gameIds == null || !gameIds.Any())
                    {
                        PrintInlineHighlight("Discovered ", gameIds.Count().ToString(), " games");
                        PrintHiglightWarning("The following user has no game screenshots: ", steamUsername);
                    }
                    else
                    {
                        PrintHiglightInfo("Discovered games with SteamApp IDs: ", "{ " + string.Join(", ", gameIds.ToArray<string>()) + " }");
                        
                        foreach (string gameId in gameIds)
                        {
                            string gameName = SteamApiUtil.GetSteamGameName(gameId);

                            if (gameName != null)
                            {
                                PrintHiglightSuccess(string.Format("Resolved game SteamApp ID {0}: ", gameId), gameName);
                                int screenshotCopyCount = FileUtil.CopyContentsToSteamShots(SteamUtil.GetGameScreenshotsFullPath(steamUserId, gameId), steamUsername, gameName);
                                PrintInlineHighlight("Copied ", screenshotCopyCount.ToString(), " screensots!");
                            }
                            else
                            {
                                PrintHiglightWarning("The following SteamApp ID couldn't resolve: ", gameId);
                            }
                        }

                        Console.WriteLine();
                    }
                }
                else
                {
                    PrintHiglightWarning("The following Steam ID couldn't resolve: ", steamUserId);
                }
            }

            PrintFinished();
            WaitForInput();
        }

        private static void PrintWelcome()
        {
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("##########################");
            Console.WriteLine("#     SteamShots.NET     #");
            Console.WriteLine("##########################");
            Console.WriteLine();

            Console.ResetColor();
        }

        private static void PrintFinished()
        {
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("##########################");
            Console.WriteLine("#        SUCCESS!        #");
            Console.WriteLine("##########################");
            Console.WriteLine();

            Console.ResetColor();
        }

        private static void PrintHiglightInfo(string baseString, string highlight)
        {
            Console.Write(baseString);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(highlight);
            Console.ResetColor();
        }

        private static void PrintHiglightSuccess(string baseString, string highlight)
        {
            Console.Write(baseString);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(highlight);
            Console.ResetColor();
        }

        private static void PrintHiglightWarning(string baseString, string highlight)
        {
            Console.Write(baseString);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(highlight);
            Console.ResetColor();
        }

        private static void PrintHiglightError(string errorString)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(errorString);
            Console.ResetColor();
        }

        private static void PrintInlineHighlight(string prefixString, string highlight, string suffixString)
        {
            Console.Write(prefixString);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(highlight);
            Console.ResetColor();
            Console.WriteLine(suffixString);
        }

        private static void WaitForInput()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}
