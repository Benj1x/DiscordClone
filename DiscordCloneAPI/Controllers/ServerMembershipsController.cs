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
        public async Task<ActionResult<ServerMembership>> GetServerMembership(long id)
        {
            var serverMembership = await _context.Memberships.FindAsync(id);

            if (serverMembership == null)
            {
                return NotFound();
            }

            return serverMembership;
        }

        // PUT: api/ServerMemberships/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")] Probably won't need this
        //public async Task<IActionResult> PutServerMembership(long id, ServerMembership serverMembership)
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
            _context.Memberships.Add(serverMembership);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetServerMembership", new { id = serverMembership.RelationID }, serverMembership);
        }

        // DELETE: api/ServerMemberships/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteServerMembership(long id)
        {
            var serverMembership = await _context.Memberships.FindAsync(id);
            if (serverMembership == null)
            {
                return NotFound();
            }

            _context.Memberships.Remove(serverMembership);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ServerMembershipExists(long id)
        {
            return _context.Memberships.Any(e => e.RelationID.Equals(id));
        }
    }
}
