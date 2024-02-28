using DiscordCloneAPI.DBContexts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Net;
using System.Web.Mvc;

namespace DiscordCloneAPI.Utilities.Functions
{
    public class UServerMembership
    {
        private readonly MembershipContext _context;
        public UServerMembership(MembershipContext context)
        {
            _context = context;
        }

        /// <summary>
        /// <c>GetMemberships</c> Gets all memberships for a server.
        /// </summary>
        /// <returns>A List of <c>ServerMembership</c></returns>
        public async Task<ActionResult<IEnumerable<ServerMembership>>> GetMemberships(long ServerID)
        {
            return await _context.Memberships.Where(e => e.ServerID == ServerID).ToListAsync();
        }

        //Get specific membership
        public async Task<ActionResult<ServerMembership>> GetServerMembership(long RelationID)
        {
            var serverMembership = await _context.Memberships.FindAsync(RelationID);

            return serverMembership;
        }

        public async Task<ActionResult<ServerMembership>> GetUserMemberships(long UserID)
        {
            var serverMembership = await _context.Memberships.FindAsync(UserID);

            return serverMembership;
        }

        public List<Server> GenerateRandomServers()
        {
            var random = new Random();
            var servers = new List<Server>();

            for (int i = 0; i < 25; i++)
            {
                var server = new Server
                {
                    ServerID = random.Next(1, 10000),
                    OwnerID = random.Next(1, 10000),
                    ServerName = $"Server{random.Next(1, 10000)}",
                    FormServerIcon = null,
                    AFKChannelID = random.Next(1, 10000),
                    AFKTimeout = random.Next(1, 10000),
                    ServerRegion = $"Region{random.Next(1, 100)}"
                };

                servers.Add(server);
            }

            return servers;
        }

        /// <summary>
        /// <c>PostServerMembership</c> adds a user to a server.
        /// </summary>
        /// <returns>True if successfully added false if not</returns>
        public async Task<bool> PostServerMembership(ServerMembership serverMembership)
        {
            Random random = new Random();

            random.NextInt64(0, 9223372036854775807);
            serverMembership.RelationID = random.NextInt64(0, 9223372036854775807) + 1;

            if(ServerMembershipExists(serverMembership.UserID, serverMembership.ServerID))
            {
                return false;
            }

            try 
            {
                _context.Memberships.Add(serverMembership);
            } 
            catch (Exception ex)
            {
                if (ServerMembershipIDExists(serverMembership.RelationID))
                {
                    while (ServerMembershipIDExists(serverMembership.RelationID))
                    {
                        _context.Memberships.Remove(serverMembership);
                        serverMembership.RelationID = random.NextInt64(0, 9223372036854775807) + 1;
                        _context.Memberships.Add(serverMembership);
                    }
                    _context.Memberships.Add(serverMembership);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw;
                }
            }
            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// <c>DeleteServerMembership</c> "Deletes" a relation (user left the server).
        /// </summary>
        /// <returns>true if successful, user wasn't found in the server</returns>
        /// <remarks>Add a notify/listen for the server?</remarks>
        public async Task<bool> DeleteServerMembership(long id)
        {
            var serverMembership = await _context.Memberships.FindAsync(id);
            if (serverMembership == null)
            {
                return false; //User not found in the server
            }

            _context.Memberships.Remove(serverMembership);
            await _context.SaveChangesAsync();

            return true; //Success 
        }

        public async Task<bool> DeleteAllServerMemberships(long ServerID)
        {
   
            var ServerMembers = _context.Memberships.Where(e => e.ServerID == ServerID);
            _context.Memberships.RemoveRange(ServerMembers);
            try
            {
                await _context.SaveChangesAsync();
            } catch (Exception ex)
            {
                throw;
            }
            

            return true; //Success 
        }

        /// <summary>
        /// <c>ServerMembershipIDExists</c> Checks if a relation ID already exists
        /// </summary>
        /// <returns>true if successful, user wasn't found in the server</returns>
        /// <seealso cref="ServerMembershipExists"/>
        private bool ServerMembershipIDExists(long RelationID)
        {
            return _context.Memberships.Any(e => e.RelationID == RelationID);
        }
        /// <summary>
        /// <c>ServerMembershipExists</c> Checks if a user is already in a server
        /// </summary>
        /// <returns>true if found in the server, false if they weren't found in the server</returns>
        /// <seealso cref="ServerMembershipIDExists"/>
        private bool ServerMembershipExists(long userID, long serverID)
        {
            //If any relation contains the UserID && ServerID
            return _context.Memberships.Any(e => e.UserID == userID && e.ServerID == serverID);
        }

    }
}
