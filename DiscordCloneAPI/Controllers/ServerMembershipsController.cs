using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DiscordCloneAPI.DBContexts;
using Models;

namespace DiscordCloneAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServerMembershipsController : ControllerBase
    {
        private readonly MembershipContext _context;

        public ServerMembershipsController(MembershipContext context)
        {
            _context = context;
        }

        // GET: api/ServerMemberships
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServerMembership>>> GetMemberships()
        {
            return await _context.Memberships.ToListAsync();
        }

        // GET: api/ServerMemberships/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ServerMembership>>> GetServerMembership(string id)
        {
            var serverMembership = _context.Memberships.Where(ms => ms.UserID.Equals(id)).ToList();

            if (serverMembership == null)
            {
                return NotFound();
            }

            return serverMembership;
        }

        // PUT: api/ServerMemberships/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")] Probably won't need this
        //public async Task<IActionResult> PutServerMembership(string id, ServerMembership serverMembership)
        //{
        //    if (id != serverMembership.RelationID)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(serverMembership).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ServerMembershipExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/ServerMemberships
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ServerMembership>> PostServerMembership(ServerMembership serverMembership)
        {
            //Check that both the user and server exists too

            if (ServerMembershipExists(serverMembership.UserID, serverMembership.ServerID))
            {
                return Conflict();
            }
            _context.Memberships.Add(serverMembership);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetServerMembership", new { id = serverMembership.UserID, serverMembership.ServerID }, serverMembership);
        }

        // DELETE: api/ServerMemberships/5
        [HttpDelete("{UserID}")]
        public async Task<IActionResult> DeleteServerMembership(string UserID, string serverID)
        {
            var serverMembership = await _context.Memberships.FindAsync(UserID, serverID);
            if (serverMembership == null)
            {
                return NotFound();
            }

            _context.Memberships.Remove(serverMembership);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// <c>ServerMembershipExists</c> Checks if a user is already in a server
        /// </summary>
        /// <returns>true if found in the server, false if they weren't found in the server</returns>
        private bool ServerMembershipExists(string userID, string serverID)
        {
            //If any relation contains the UserID && ServerID
            return _context.Memberships.Any(e => e.UserID.Equals(userID) && e.ServerID.Equals(serverID));
        }
    }
}
