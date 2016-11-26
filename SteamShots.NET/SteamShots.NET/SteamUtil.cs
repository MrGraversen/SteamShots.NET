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
        public static string[] GetUserProfiles(string basePath)
        {
            return Directory.GetDirectories(basePath);
        }

        public static string ConvertSteamId32BitTo64Bit(string steamId32)
        {
            return string.Format("{0}{1}", "765", (Convert.ToInt32(steamId32) + 61197960265728));
        }
    }
}
