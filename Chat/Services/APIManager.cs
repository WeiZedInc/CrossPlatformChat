﻿using CrossPlatformChat.EmulatorHelper;
using Newtonsoft.Json;
using System.Text;

namespace CrossPlatformChat.Services
{
    internal class APIManager
    {
        private static APIManager _instance;
        //private string _serverRootURL = "https://10.0.2.2:7233"; //api url, use on hosted api
        public string _accessToken = "";

        static APIManager() => _instance = new APIManager();
        private APIManager() { }
        public static APIManager Instance { get => _instance; }

        public async Task<AuthenticationResponse> Authenticate(AuthenticationRequest request, string path = "/Authentication/Authenticate")
        {
            if (request == null) return null;

            var devSsl = new DevHttpsConnectionHelper(7233); // for emulators only with localdb
            using (HttpClient client = devSsl.HttpClient)
            {
                Uri URI = new Uri(devSsl.DevServerRootUrl + path);
                var httpRequest = new HttpRequestMessage(HttpMethod.Post, URI);

                httpRequest.Content = new StringContent(JsonConvert.SerializeObject(request), encoding: Encoding.UTF8, "application/json");

                try
                {
                    client.Timeout = TimeSpan.FromSeconds(10);
                    var response = await client.SendAsync(httpRequest);
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
