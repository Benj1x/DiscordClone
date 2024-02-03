using Cassandra;
using Microsoft.EntityFrameworkCore;

namespace DiscordCloneAPI.Utilities;


    public static class DbContextWrapper
    {
        public static WebApplicationBuilder WrapDbContext<TContext>(this WebApplicationBuilder builder) where TContext : DbContext
        {
            string connectionType = "DefaultConnection";

        builder.Services.AddDbContext<TContext>(opt =>
        {
            try
            {
                opt.UseInMemoryDatabase("ServerList");
                //var cluster = Cluster.Builder()
                // .AddContactPoints(connectionType)
                // .Build();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while configuring the db: {ex.Message}\n\n Falling back to dev DB");
                opt.UseInMemoryDatabase("ServerList");
            }

        });
            return builder;
    }
    }

