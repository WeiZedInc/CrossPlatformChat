﻿using CrossPlatformChat.Database.Entities;
using SQLite;

namespace CrossPlatformChat.MVVM.Models
{
    public class ClientModel : ClientEntity
    {
        private string _loginInput;
        private string _passwordInput;
        private bool _isProcessing;
        public ICommand LoginCommand { get; set; }
        public ICommand GoToRegisterViewCommand { get; set; }
        public ICommand GoToChatsViewCommand { get; set; }

        public string LoginInput
        {
            get { return _loginInput; }
            set { _loginInput = value; OnPropertyChanged(); }
        }
        public string PasswordInput
        {
            get { return _passwordInput; }
            set { _passwordInput = value; OnPropertyChanged(); }
        }
        public bool IsProcessing
        {
            get { return _isProcessing; }
            set { _isProcessing = value; OnPropertyChanged(); }
        }

        public ClientModel()
        {
            LoginInput = string.Empty;
            PasswordInput = string.Empty;
            IsProcessing = false;
        }
    }
}
