using CrossPlatformChat.Helpers;
using CrossPlatformChat.Services.Base;
using Newtonsoft.Json;
using System.Text;

namespace CrossPlatformChat.Services
{
    public enum RequestPath
    {
        Authenticate,
        Register,
        GetUserByUsername,
        CreateChat
    }

    public class APIManager
    {
        public async Task<T> HttpRequest<T>(object request, RequestPath pathEnum, HttpMethod method) where T : BaseResponse, new()
        {
            if (request == null) return null;
            var devHTTPS = new DevHttpsConnectionHelper(7233);

            using (HttpClient client = devHTTPS.HttpClient)
            {

                Uri URI = new Uri(devHTTPS.DevServerRootUrl + GetAPIPath(pathEnum));
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
                RequestPath.GetUserByUsername => "/GeneralUsers/GetByUsername",
                RequestPath.CreateChat => "/ChatCreation/CreateNewChat",
                _ => string.Empty
            };
        }
    }
}
