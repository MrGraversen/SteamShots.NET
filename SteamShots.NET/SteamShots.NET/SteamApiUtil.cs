using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        private const string steamBaseStoreApiUrl = "http://store.steampowered.com/api/appdetails?appids=";

        public static string GetSteamUsername(string steamApiKey, string steamId32Bit)
        {
            string steamId64Bit = SteamUtil.ConvertSteamId32BitTo64Bit(steamId32Bit);

            string apiUrl = string.Format("{0}/ISteamUser/GetPlayerSummaries/v0002/?key={1}&steamids={2}", steamBaseApiUrl, steamApiKey, steamId64Bit);

            try
            {
                WebClient webClient = new WebClient();
                string jsonString = webClient.DownloadString(apiUrl);

                dynamic dynamicJsonObject = JsonConvert.DeserializeObject(jsonString);

                return dynamicJsonObject.response.players[0].personaname;
            }
            catch
            {
                return null;
            }
        }

        public static string GetSteamGameName(string steamAppId)
        {
            string apiUrl = string.Format("{0}{1}", steamBaseStoreApiUrl, steamAppId);

            try
            {
                WebClient webClient = new WebClient();
                string jsonString = webClient.DownloadString(apiUrl);

                JObject jsonObject = JObject.Parse(jsonString);
                JToken gameNameJsonToken = jsonObject[steamAppId]["data"]["name"];

                return gameNameJsonToken.ToString();
            }
            catch
            {
                return null;
            }
        }
    }
}
