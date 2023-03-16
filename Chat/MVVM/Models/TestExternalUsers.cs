using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatformChat.MVVM.Models
{
    public class TestExternalUsers
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Username { get; set; }
        public string AvatarSource { get; set; }
        public bool IsOnline { get; set; }
        public DateTime LastLoginTime { get; set; }
    }
}
