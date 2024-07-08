using Models;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Discord.Settings
{
    public class APIEndpoints
    {
        private static string _endpoint = "https://localhost:7210/api/";

        public async Task<HttpResponseMessage> TryLogin(string email, string password)
        {
            string requestContent = $"{{\r\n  \"email\": \"{email}\",\r\n  \"password\": \"{password}\"\r\n}}";
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, _endpoint + "Users/login");
            request.Headers.Add("accept", "text/plain");
            var content = new StringContent(requestContent, null, "application/json");
            request.Content = content;
            return await client.SendAsync(request);
        }

        public static async Task<User> GetUser(string uID)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, _endpoint+"Users/"+$"{uID}");
            request.Headers.Add("accept", "text/plain");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStreamAsync();
            User? user = await JsonSerializer.DeserializeAsync<User>(responseStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            //if (user == null)
            //{
            //    return 
            //}

            return user;
        }

        public static async Task<List<string>> GetMyServers()
        {
            string id = AuthSingleton.GetAuthSingleton().GetUserID();
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, _endpoint + "ServerMemberships/" + $"{id}");

            request.Headers.Add("accept", "text/plain");
            var response = await client.SendAsync(request);

            Console.Write(response.Content);

            response.EnsureSuccessStatusCode();

            var responseStream = await response.Content.ReadAsStreamAsync();

            string res = await response.Content.ReadAsStringAsync();
            string serverPattern = "\"serverID\":\"[a-zA-Z0-9]*\"";
            MatchCollection serverMatches;
            
            List<string> servers = new List<string>();

            serverMatches = Regex.Matches(res, serverPattern);

            foreach (var server in serverMatches)
            {
                string temp = server.ToString().Remove(0, 12);
                servers.Add(temp.Remove(temp.Length - 1, 1));
            }

            return servers;
        }
        public static async Task<List<Server>> GetServers(List<string> ids)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, _endpoint + "Servers/List");
            request.Headers.Add("accept", "text/plain");
            string contentString = "[\n";
            foreach (var id in ids)
            {
                if (id.Equals(ids[ids.Count-1]))
                {
                    contentString += "\"" + id + "\"\n]";
                }
                else
                {
                    contentString += "\"" + id + "\",\n";
                }
            }
            //var contentString = JsonSerializer.Serialize(ids);

            var content = new StringContent(contentString, null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);

            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStreamAsync();

            List<Server>? servers = await JsonSerializer.DeserializeAsync<List<Server>>(responseStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return servers;
        }

        public static async Task<Server> GetServer(string serverID)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, _endpoint + "Servers/" + $"{serverID}");
            request.Headers.Add("accept", "text/plain");
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseStream = await response.Content.ReadAsStreamAsync();
            Server server = await JsonSerializer.DeserializeAsync<Server>(responseStream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            return server;
        }

    }

}
