using CrossPlatformChat.EmulatorHelper;
using Newtonsoft.Json;
using System.Text;

namespace CrossPlatformChat.Services
{
    internal class ServiceProvider
    {
        private static ServiceProvider _instance;
        //private string _serverRootURL = "https://10.0.2.2:7233"; //api url, use on hosted api
        public string _accessToken = "";

        static ServiceProvider() => _instance = new ServiceProvider();
        private ServiceProvider() { }
        public static ServiceProvider Instance { get => _instance; }

        public async Task<AuthenticationResponse> Authenticate(AuthenticationRequest request, string path = "/Authentication/Authenticate")
        {
            var devSsl = new DevHttpsConnectionHelper(7233); // for emulators only with localdb
            using (HttpClient client = devSsl.HttpClient)
            {
                client.Timeout = TimeSpan.FromSeconds(10);
                var httpRequestMsg = new HttpRequestMessage();
                httpRequestMsg.Method = HttpMethod.Post;
                httpRequestMsg.RequestUri = new Uri(devSsl.DevServerRootUrl + path);

                if (request != null)
                {
                    string jsonContent = JsonConvert.SerializeObject(request);
                    var httpContent = new StringContent(jsonContent, encoding: Encoding.UTF8, "application/json");
                    httpRequestMsg.Content = httpContent;
                }

                try
                {
                    var response = await client.SendAsync(httpRequestMsg);
                    var responseContent = await response.Content.ReadAsStringAsync();

                    var result = JsonConvert.DeserializeObject<AuthenticationResponse>(responseContent);
                    result.StatusCode = (int)response.StatusCode;

                    if (result.StatusCode == 200)
                        _accessToken = result.Token;

                    return result;
                }
                catch (Exception ex)
                {
                    return new AuthenticationResponse
                    {
                        StatusCode = 500,
                        StatusMessage = ex.Message
                    };
                }
            }
        }
    }
}
