using Microsoft.EntityFrameworkCore;
using Models;

namespace DiscordCloneAPI.DBContexts
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
            : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
