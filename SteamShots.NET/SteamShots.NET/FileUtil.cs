using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SteamShots.NET
{
    class FileUtil
    {
        private readonly static string apiKeyFileName = "steam_api_key.txt";
        private readonly static string desktopFilePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        private readonly static string screenshotFolderName = "SteamShots.NET";

        public static bool ApiKeyFileExists()
        {
            return File.Exists(apiKeyFileName);
        }

        public static string CreateApiKeyFile()
        {
            File.Create(apiKeyFileName);
            return apiKeyFileName;
        }

        public static string ReadApiKey()
        {
            return File.ReadAllText(apiKeyFileName);
        }

        public static void CreateSteamShotsDirectory()
        {
            string fullPath = string.Format("{0}\\{1}", desktopFilePath, screenshotFolderName);

            if (!Directory.Exists(fullPath))
            {
                Directory.CreateDirectory(fullPath);
            }
        }

        public static int CopyContentsToSteamShots(string steamPath, string steamName, string gameName)
        {
            gameName = CorrectGameName(gameName);

            string targetPath = string.Format("{0}\\{1}", desktopFilePath, screenshotFolderName);

            int screenshotCount = Directory.GetFiles(steamPath).Length;

            foreach (string screenshotFile in Directory.GetFiles(steamPath))
            {
                string steamShotsDirectory = Path.Combine(targetPath, steamName, gameName);

                if (!Directory.Exists(steamShotsDirectory))
                {
                    Directory.CreateDirectory(CorrectFilePath(steamShotsDirectory));
                }

                string finalTargetPath = CorrectFilePath(Path.Combine(steamShotsDirectory, Path.GetFileName(screenshotFile)));

                if (!File.Exists(finalTargetPath))
                {
                    File.Copy(screenshotFile, finalTargetPath);
                }
            }

            return screenshotCount;
        }

        private static string CorrectFilePath(string path)
        {
            foreach (char c in Path.GetInvalidPathChars())
            {
                path = path.Replace(c.ToString(), "");
            }

            return path;
        }

        private static string CorrectGameName(string gameName)
        {
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                gameName = gameName.Replace(c.ToString(), "");
            }

            return gameName;
        }
    }
}