
namespace Discord.Settings
{
    public class APIEndpoints
    {
        private string _endpoint = "https://localhost:7210/api/";

        public async Task<HttpResponseMessage> TryLogin(string email, string password)
        {
            string requestContent = $"{{\r\n  \"email\": \"{email}\",\r\n  \"password\": \"{password}\"\r\n}}";
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, _endpoint+"Users/login");
            request.Headers.Add("accept", "text/plain");
            var content = new StringContent(requestContent, null, "application/json");
            request.Content = content;
            return await client.SendAsync(request);
        }
        public async Task<HttpResponseMessage> GetMyServers()
        {
            string id = AuthSingleton.GetAuthSingleton().GetUserID();
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, _endpoint + "ServerMemberships/" + $"{id}");
            //request.Headers.Add("accept", "text/plain");
            request.Headers.Add("accept", "*/*");
            return await client.SendAsync(request);
        }
        public async Task<HttpResponseMessage> GetServers()
        {
            string id = AuthSingleton.GetAuthSingleton().GetUserID();
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, _endpoint + "ServerMemberships/" + $"{id}");
            //request.Headers.Add("accept", "text/plain");
            request.Headers.Add("accept", "*/*");
            return await client.SendAsync(request);
        }
    }
    
}
