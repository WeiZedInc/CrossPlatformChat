using CrossPlatformChat.MVVM.Models.Users;
using CrossPlatformChat.Services.Database;
using CrossPlatformChat.Utils;
using static System.Net.Mime.MediaTypeNames;

namespace CrossPlatformChat.Services
{
    public class ClientManager
    {
        static ClientManager _instance;
        public ClientData? Local;
        public List<GeneralUserData> ExternalUsersList;
        ISQLiteService db;

        public static ClientManager Instance { 
            get 
            {
                if (_instance == null)
                {
                    GetLocalData();
                    //GetExternalData();
                }

                return _instance; 
            } 
        }
        private ClientManager() { }

        static void GetLocalData()
        {
            try
            {
                _instance = new ClientManager();
                _instance.db = ServiceHelper.GetService<ISQLiteService>();
                var clientData = _instance.db.FirstOrDefault<ClientData>().Result;

                _instance.Local = clientData;
            }
            catch (Exception)
            {
                throw;
            }
        }

        static void GetExternalData()
        {
            _instance.ExternalUsersList = new List<GeneralUserData>();
        }

        //static async Task<bool> TryLoginAsync()
        //{
        //    try
        //    {
        //        var request = new AuthenticationRequest
        //        {
        //            Login = LoginInput,
        //            HashedPassword = ClientManager.Instance.Local.HashedPassword
        //        };
        //        var response = await APIManager.Instance.Authenticate(request);
        //        if (response.StatusCode == 200)
        //        {
        //            Test = $"Logined!\nUsername: {response.UserName}\nToken:{response.Token}";
        //            return true;
        //        }
        //        else
        //        {
        //            Test = response.StatusMessage;
        //            return false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Test = ex.Message;
        //        return false;
        //    }
        //    finally
        //    {
        //        IsProcessing = false;
        //    }
        //}
    }
}
