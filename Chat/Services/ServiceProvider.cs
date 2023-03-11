using CrossPlatformChat.EmulatorHelper;
using Newtonsoft.Json;
using System.Text;

namespace CrossPlatformChat.Services
{
    internal class ServiceProvider
    {
        private static ServiceProvider _instance;
        private string _serverRootURL = "https://10.0.2.2:7093";
        public string _accessToken = "";

        static ServiceProvider() => _instance = new ServiceProvider();
        private ServiceProvider() { }
        public static ServiceProvider Instance { get => _instance; }

        public async Task<AuthenticationResponse> Authenticate(AuthenticationRequest request)
        {
            var devSsl = new DevHttpsConnectionHelper(7093); // for emulators only with localdb
            using (HttpClient client = devSsl.HttpClient)
            {
                client.Timeout = TimeSpan.FromSeconds(5);
                var httpRequestMsg = new HttpRequestMessage();
                httpRequestMsg.Method = HttpMethod.Post;
                httpRequestMsg.RequestUri = new Uri(devSsl.DevServerRootUrl + "/Authenticate/Authenticate");

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
                    var result = new AuthenticationResponse
                    {
                        StatusCode = 500,
                        StatusMessage = ex.Message
                    };
                    return result;
                }
            }
        }
    }
}
