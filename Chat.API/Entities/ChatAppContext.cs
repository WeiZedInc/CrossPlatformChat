using Microsoft.EntityFrameworkCore;

namespace Chat.API.Entities
{
    public class ChatAppContext : DbContext
    {
        public ChatAppContext(DbContextOptions<ChatAppContext> options) : base(options) { }
        public virtual DbSet<Users> Users { get; set; } = null!;
        public virtual DbSet<Chats> Chats { get; set; } = null!;

        public static readonly string connectionString = "Server=localhost; User ID=root; Password=root; Database=chatapp";
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }
    }
}