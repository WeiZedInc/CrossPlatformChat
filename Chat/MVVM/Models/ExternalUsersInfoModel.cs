using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrossPlatformChat.MVVM.Models
{
    [Table("users")]
    internal class ExternalUsersInfoModel
    {
        [Key] public int ID { get; set; }
        public string Login { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string AvatarSource { get; set; } = "avatar.png";
        public bool IsOnline { get; set; }
        public DateTime LastLoginTime { get; set; }
    }
}
