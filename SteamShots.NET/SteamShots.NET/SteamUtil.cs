using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamShots.NET
{
    class SteamUtil
    {
        public static IEnumerable<string> GetUserProfiles()
        {
            string steamPath = string.Format("{0}\\{1}", RegistryUtil.GetSteamInstallPath(), "userdata");
            foreach (string directory in Directory.GetDirectories(steamPath))
            {
                yield return new DirectoryInfo(directory).Name;
            }
        }

        public static IEnumerable<string> GetGamesWithScreenshots(string steamId32Bit)
        {
            string screenshotsPath = string.Format("{0}\\{1}\\{2}\\{3}", RegistryUtil.GetSteamInstallPath(), "userdata", steamId32Bit, "760\\remote");

            if (!Directory.Exists(string.Format(screenshotsPath)))
            {
                yield break;
            }

            foreach (string directory in Directory.GetDirectories(screenshotsPath))
            {
                yield return new DirectoryInfo(directory).Name;
            }
        }

        public static string GetGameScreenshotsFullPath(string steamId32Bit, string gameId)
        {
            return string.Format("{0}\\{1}\\{2}\\{3}\\{4}\\{5}", RegistryUtil.GetSteamInstallPath(), "userdata", steamId32Bit, "760\\remote", gameId, "screenshots");
        }

        public static string ConvertSteamId32BitTo64Bit(string steamId32)
        {
            return string.Format("{0}{1}", "765", (Convert.ToInt32(steamId32) + 61197960265728));
        }
    }
}
