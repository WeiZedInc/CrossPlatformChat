﻿using CrossPlatformChat.Services.Base;

namespace CrossPlatformChat.Services.Authentication
{
    internal class AuthenticationRequest : IBaseRequest
    {
        public string Login { get; set; }
        public string HashedPassword { get; set; }
    }
}
