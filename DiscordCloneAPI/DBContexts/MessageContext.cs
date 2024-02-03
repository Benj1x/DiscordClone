using Microsoft.EntityFrameworkCore;
using Models;

namespace DiscordCloneAPI.DBContexts
{
    public class MessageContext : DbContext
    {
        public MessageContext(DbContextOptions<MessageContext> options)
            : base(options)
        {
        }
        public DbSet<Message> Messages { get; set; }
    }
}
