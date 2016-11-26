using SteamShots.NET.ApiObjects;
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
            PlayerSummary playerSummary = SteamApiUtil.GetPlayerSummary(FileUtil.ReadApiKey(), SteamUtil.ConvertSteamId32BitTo64Bit("35686634"));
            Console.ReadKey();
        }
    }
}
