using CrossPlatformChat.MVVM.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossPlatformChat.Services
{
    internal class ChatAppContext : DbContext
    {
        public ChatAppContext(DbContextOptions<ChatAppContext> options) : base(options) { }
        public virtual DbSet<ChatInfoModel> Chats { get; set; } = null!;
        public virtual DbSet<CurrentUserInfoModel> Users { get; set; } = null!;
        public virtual DbSet<MessageInfoModel> Messages { get; set; } = null!;

        public static readonly string connectionString = "Data Source=localapp.db";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString);
        }
    }
}
