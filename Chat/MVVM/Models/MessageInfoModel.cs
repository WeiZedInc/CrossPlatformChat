using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CrossPlatformChat.MVVM.Models
{
    public enum MessageStatus
    {
        Read = 0,
        Received,
        SentAndNotRead
    }

    [Table("messages")]
    internal class MessageInfoModel
    {
        [Key] public int ID { get; set; }
        [Required] public string SenderID { get; set; }
        [Required] public int ChatID { get; set; }
        [Required][MaxLength(2020)] public string Content { get; set; } // 20 reserved for special symbols, 2k length is optimal maximum for messages
        public DateTime SentDate { get; set; }
        [EnumDataType(typeof(MessageStatus))] public MessageStatus Status { get; set; }
    }
}
