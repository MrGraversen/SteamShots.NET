using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamShots.NET
{
    class RegistryUtil
    {
        private const string userRoot = "HKEY_CURRENT_USER";
        private static string steamInstallPathCached = null;

        public static string GetSteamInstallPath()
        {
            return steamInstallPathCached ?? (steamInstallPathCached = (string) Registry.GetValue(string.Format("{0}\\{1}", userRoot, "Software\\Valve\\Steam"), "SteamPath", null));
        }
    }
}
