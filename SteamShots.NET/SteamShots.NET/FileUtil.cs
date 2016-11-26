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
        private const string apiKeyFileName = "steam_api_key.txt";

        public static bool ApiKeyFileExists()
        {
            return File.Exists(apiKeyFileName);
        }

        public static void CreateApiKeyFile()
        {
            File.Create(apiKeyFileName);
        }

        public static string ReadApiKey()
        {
            return File.ReadAllText(apiKeyFileName);
        }
    }
}