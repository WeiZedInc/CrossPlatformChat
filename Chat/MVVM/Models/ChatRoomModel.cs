using CrossPlatformChat.Database;
using CrossPlatformChat.Database.Entities;
using CrossPlatformChat.Utils.Helpers;
using System.Collections.ObjectModel;

namespace CrossPlatformChat.MVVM.Models
{
    public class ChatRoomModel
    {
        protected ChatEntity Chat { get; set; }
        protected bool _isSending = false;
        protected ISQLiteService _db;

        public ChatHub ChatHub { get; set; }
        public GeneralUserEntity User { get; set; }
        public ObservableCollection<MessageEntity> Messages { get; set; }
        public string MessageToEncrypt { get; set; }
        public bool IsRefreshing { get; set; }

        public ICommand SendMsgCMD { get; set; }
        public ICommand RefreshCMD { get; set; }
    }
}
