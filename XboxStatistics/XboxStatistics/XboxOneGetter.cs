using System;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using XboxStatistics.Models;

namespace XboxStatistics
{
    public class XboxOneGetter
    {
        private const string Xuid = "2533274845176708";

        public XboxOneGameCollection GetMyGames()
        {
            return CachedRead<XboxOneGameCollection>($"/v2/{Xuid}/xboxonegames", "all.json");
        }

        public XboxOneGame GetGameDetails(int titleId)
        {
            var hex = ToHex(titleId);
            return CachedRead<XboxOneGame>($"/v2/game-details-hex/{hex}", $"{hex}.details.json");
        }

        public GameStats GetGameStats(int titleId)
        {
            var hex = ToHex(titleId);
            return CachedRead<GameStats>($"/v2/{Xuid}/game-stats/{titleId}", $"{hex}.stats.json");
        }

        public Achievement[] GetGameAchievements(int titleId)
        {
            var hex = ToHex(titleId);
            return CachedRead<Achievement[]>($"/v2/{Xuid}/achievements/{titleId}", $"{hex}.achievements.json");
        }

        private T CachedRead<T>(string url, string file)
        {
            string json;
            if (File.Exists(file)) json = File.ReadAllText(file);
            else
            {
                using (var client = InitWebClient())
                {
                    json = client.DownloadString(url);
                    File.WriteAllText(file, json);
                }
            }
            return JsonConvert.DeserializeObject<T>(json);
        }

        private WebClient InitWebClient()
        {
            var client = new WebClient {BaseAddress = "https://xboxapi.com"};
            client.Headers.Add("X-AUTH", "00fc4fd199eec9e7d31cfa4653c254ef78eca41b");
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            return client;
        }

        private string ToHex(int id)
        {
            var a = BitConverter.GetBytes(id);
            SwapEndian(a, 4);
            return string.Join(string.Empty, a.Select(b => b.ToString("X2")));
        }

        public static void SwapEndian(byte[] a, int div)
        {
            var temp = new byte[div];
            for (var i = 0; i < a.Length; i += div)
            {
                Buffer.BlockCopy(a, i, temp, 0, div);
                Array.Reverse(temp);
                Buffer.BlockCopy(temp, 0, a, i, div);
            }
        }
    }
}