using CrossPlatformChat.Services.Base;
using CrossPlatformChat.Utils.Helpers;
using Newtonsoft.Json;
using System.Text;

namespace CrossPlatformChat.Services
{
    public enum RequestPath
    {
        Authenticate,
        Register,
        GetUserByUsername,
    }

    internal class APIManager
    {
        private static APIManager _instance;
        //private string _serverRootURL = "https://10.0.2.2:7233"; //api url, use on hosted api
        public string _accessToken = "";

        static APIManager() => _instance = new APIManager();
        private APIManager() { }
        public static APIManager Instance { get => _instance; }

        public async Task<T> HttpRequest<T>(BaseRequest request, RequestPath pathEnum, HttpMethod method) where T : BaseResponse, new()
        {
            if (request == null) return null;

            var devSsl = new DevHttpsConnectionHelper(7233); // for emulators only with localdb
            using (HttpClient client = devSsl.HttpClient)
            {

                Uri URI = new Uri(devSsl.DevServerRootUrl + GetAPIPath(pathEnum));
                var httpRequest = new HttpRequestMessage(method, URI);

                httpRequest.Content = new StringContent(JsonConvert.SerializeObject(request), encoding: Encoding.UTF8, "application/json");

                try
                {
                    client.Timeout = TimeSpan.FromSeconds(10);
                    var response = await client.SendAsync(httpRequest);
                    string responseContent = await response.Content.ReadAsStringAsync();

                    T result = JsonConvert.DeserializeObject<T>(responseContent);
                    result.StatusCode = (int)response.StatusCode;

                    if (result.StatusCode == 200)
                        return result;
                    else
                        return null;
                }
                catch (Exception ex)
                {
                    return new T
                    {
                        StatusCode = 500,
                        StatusMessage = ex.Message
                    };
                }
            }
        }

        string GetAPIPath(RequestPath pathEnum)
        {
            return pathEnum switch
            {
                RequestPath.Authenticate => "/Authentication/Authenticate",
                RequestPath.Register => "/Registration/Register",
                RequestPath.GetUserByUsername => "/Users/GetByUsername",
                _ => string.Empty
            };
        }
    }
}
