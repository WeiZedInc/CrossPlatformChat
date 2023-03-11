using Newtonsoft.Json;
using System.Text;

namespace CrossPlatformChat.Services
{
    internal class ServiceProvider
    {
        private static ServiceProvider _instance;
        private string _serverRootURL = "https://10.0.2.2:7233";
        public string _accessToken = "";

        private ServiceProvider() { }

        public static ServiceProvider Instance
        {
            get
            {
                if (_instance == null) { _instance = new ServiceProvider(); }
                return _instance;
            }
        }

        public async Task<AuthenticationResponse> Authenticate(AuthenticationRequest request)
        {
            using (HttpClient client = new HttpClient())
            {
                var httpRequestMsg = new HttpRequestMessage();
                httpRequestMsg.Method = HttpMethod.Post;
                httpRequestMsg.RequestUri = new Uri(_serverRootURL + "/Authenticate/Authenticate");

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
                    {
                        _accessToken = result.Token;
                    }

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
