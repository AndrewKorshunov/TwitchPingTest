using System;
using System.Net;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace TwitchLibrary
{
    static public class TwitchServerParser
    {
        private const string apiUrl = @"https://api.twitch.tv/kraken/ingests";

        static public async Task<IEnumerable<TwitchServer>> GetAllTwitchServers()
        {
            var serverList = new List<TwitchServer>();
            var json = await GetJsonAnswer().ConfigureAwait(false);
            dynamic jsonServersObject = JsonConvert.DeserializeObject(json);
            foreach (var server in jsonServersObject.ingests)
            {
                serverList.Add(new TwitchServer()
                {
                    Name = server.name,
                    Availability = server.availability,
                    Id = server._id,
                    Url = GetHostnameFromUrl(server.url_template.ToString())
                });
            }
            return serverList;
        }

        static private string GetHostnameFromUrl(string urlTemplate)
        {
            // example "rtmp://live-mia.twitch.tv/app/{stream_key}"
            var result = urlTemplate
                .SkipWhile(x => x != '/')  // skip protocol
                .Skip(2)  // 
                .TakeWhile(x => x != '/');  // take only host address
            return new string(result.ToArray());
        }

        static private async Task<string> GetJsonAnswer()
        {
            var request = WebRequest.Create(apiUrl);
            var response = await request.GetResponseAsync().ConfigureAwait(false);
            string answer = new StreamReader(response.GetResponseStream()).ReadToEnd();

            return answer;
        }
    }
}
