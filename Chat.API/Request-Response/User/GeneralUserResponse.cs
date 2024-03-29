﻿namespace Chat.API.Managers.User.Data
{
    public class GeneralUserResponse
    {
        public int ID { get; set; }
        public string Username { get; set; }
        public string AvatarSource { get; set; } = "avatar.png";
        public bool IsOnline { get; set; }
        public DateTime LastLoginTime { get; set; }
    }
}
