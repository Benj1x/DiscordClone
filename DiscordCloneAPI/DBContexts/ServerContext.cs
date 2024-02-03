using Microsoft.EntityFrameworkCore;
using Models;

namespace DiscordCloneAPI.DBContexts
{
    public class ServerContext : DbContext
    {
        public ServerContext(DbContextOptions<ServerContext> options)
            : base(options)
        {
        }
        public DbSet<Server> Servers { get; set; } = null!;
    }
}
