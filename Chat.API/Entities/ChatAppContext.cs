using Microsoft.EntityFrameworkCore;

namespace Chat.API.Entities
{
    public class ChatAppContext : DbContext
    {
        public ChatAppContext(DbContextOptions<ChatAppContext> options) : base(options) { }

        public virtual DbSet<Users> Users { get; set; } = null!;
    }
}
