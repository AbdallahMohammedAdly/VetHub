using Microsoft.EntityFrameworkCore;
using SignalR.Models;

namespace SignalR.Contexts
{
    public class ChatDbContext : DbContext
    {
        public ChatDbContext(DbContextOptions options) : base(options) 
        {
            
        }

        public DbSet<Message> messages { get; set; }
    }
}
