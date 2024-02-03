using Microsoft.EntityFrameworkCore;
using Models;

namespace DiscordCloneAPI.DBContexts
{
    public class MembershipContext : DbContext
    {
        public MembershipContext(DbContextOptions<MembershipContext> options)
            : base(options)
        {
        }
        public DbSet<ServerMembership> Memberships { get; set; } = null!;
    }
}
