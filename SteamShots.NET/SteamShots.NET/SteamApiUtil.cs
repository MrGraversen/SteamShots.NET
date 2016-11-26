using Newtonsoft.Json;
using SteamShots.NET.ApiObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SteamShots.NET
{
    class SteamApiUtil
    {
        private const string steamBaseApiUrl = "http://api.steampowered.com/";

        public static string GetSteamUsername(string steamApiKey, string steamId32Bit)
        {
            string steamId64Bit = SteamUtil.ConvertSteamId32BitTo64Bit(steamId32Bit);

            string apiUrl = string.Format("{0}/ISteamUser/GetPlayerSummaries/v0002/?key={1}&steamids={2}", steamBaseApiUrl, steamApiKey, steamId64Bit);

            WebClient webClient = new WebClient();
            string jsonString = webClient.DownloadString(apiUrl);

            dynamic dynamicJsonObject = JsonConvert.DeserializeObject(jsonString);

            return dynamicJsonObject.response.players[0].personaname;
        }
    }
}
