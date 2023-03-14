using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrossPlatformChat.MVVM.Models
{
    [Table("chats")]
    internal class ChatInfoModel
    {
        [Key] public int ID { get; set; }
        public int MissedMessagesCount { get; set; } = 0;
        [Required][MaxLength(50)] public string Name { get; set; }
        public string UersID { get; set; } //convert array of users id's to JSON
        public string MessagesID { get; set; } //convert array of messages id's to JSON
        public DateTime CreatedDate { get; set; } 
    }
}
