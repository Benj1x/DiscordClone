using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DiscordCloneAPI.DBContexts;
using Models;
using DiscordCloneAPI.Utilities.Functions;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using NuGet.Protocol;

namespace DiscordCloneAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServersController : ControllerBase
    {
        private readonly ServerContext _context;
        private readonly UServerMembership _uServerMembership;

        public ServersController(ServerContext context, UServerMembership uServerMembership)
        {
            _context = context;
            _uServerMembership = uServerMembership;
        }

        // GET: api/Servers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Server>>> GetServers()
        {
            return await _context.Servers.ToListAsync();
        }

        // GET: api/Servers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Server>> GetServer(string id)
        {
            var server = await _context.Servers.FindAsync(id);

            if (server == null)
            {
                return NotFound();
            }

            return server;
        }

        // PUT: api/Servers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutServer(Int64 id, Server server)
        {
            if (id != server.ServerID)
            {
                return BadRequest();
            }
        
            _context.Entry(server).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Servers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [SwaggerOperation(
            Summary = "Creates a new server",
            Description = "Requires a authorization key",
            OperationId = "PostServer"
        //Tags = new[] { "Search", "User" }
        )]
        [Consumes("application/json", "text/json", "image/png", "multipart/form-data")]
        [Produces("application/json", "text/json", "image/png")]
        [SwaggerResponse(200, "OK", typeof(IEnumerable<User>))]
        public async Task<ActionResult<Server>> PostServer([FromForm]Server server)
        {
            //--Read image
            if (server.FormServerIcon != null)
            {
                var memoryStream = new MemoryStream();
                await server.FormServerIcon.CopyToAsync(memoryStream);

                if (memoryStream.Length > 50000000)
                {
                    return BadRequest("Image too large");
                }
                server.ServerIcon = memoryStream.ToArray();
            }
            
            //---
            Random random = new Random();

            server.ServerID = random.NextInt64(0, 9223372036854775807);
            /*--Mock image*/
            FileStream stream = new FileStream("A:\\Kodning Git\\DiscordClone\\DiscordCloneAPI\\Controllers\\Image_created_with_a_mobile_phone.png", FileMode.Open, FileAccess.Read);
            var memStream = new MemoryStream();
            await stream.CopyToAsync(memStream);
            server.ServerIcon = memStream.ToArray();
            /*-----------------------------------------*/
            _context.Servers.Add(server);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                if (ServerExists(server.ServerID))
                {
                    while (ServerExists(server.ServerID))
                    {
                        _context.Servers.Remove(server);
                        server.ServerID = random.NextInt64(0, 9223372036854775807) + 1;
                    }
                    _context.Servers.Add(server);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw;
                }
            }

            //Instantiate new ServerMembership for owner
            ServerMembership serverMembership = new ServerMembership();
            serverMembership.ServerID = server.ServerID;
            serverMembership.UserID = server.OwnerID;
            serverMembership.RelationID = 0;

            if (!await _uServerMembership.PostServerMembership(serverMembership))
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetServer), new { id =  server.ServerID }, server);;
        }

        [HttpPatch("{ServerID}")]
        public async Task<IActionResult> PatchServer([FromQuery(Name ="UserID"), SwaggerParameter(Required = true)]long UserID, long ServerID, PATCHServer patchServer)
        {
            if (!ServerExists(patchServer.ServerID)) { 
                return BadRequest(); 
            }

            Server server = await _context.Servers.FindAsync(patchServer.ServerID);

            /*We know who you are, and you aren't premitted to do this*/
            //if (server.OwnerID != UserID || server.Admins != null && !server.Admins.Contains(UserID))
            //{
            //    return Forbid();
            //}

            //Owner has changed
            if (patchServer.OwnerID != null && !server.OwnerID.Equals(patchServer.OwnerID))
            {
                server.OwnerID = (long)patchServer.OwnerID;
            }
            
            if (patchServer.ServerName != null)
            {
                server.ServerName = patchServer.ServerName;
            }

            if (patchServer.ServerIcon != null)
            {
                server.ServerIcon = patchServer.ServerIcon;
            }

            /*Edit admins later*/

            _context.Entry(server).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Servers/5
        [HttpDelete/*("{id}")*/]
        public async Task<IActionResult> DeleteServer()
        {
            //var server = await _context.Servers.FindAsync(id);
            //if (server == null)
            //{
            //    return NotFound();
            //}

            //_context.Servers.Remove(server);
            //await _context.SaveChangesAsync();
            List<Server> x = _uServerMembership.GenerateRandomServers();
            foreach (Server server in x)
            {
                await PostServer(server);
            }
            return NoContent();
        }

        private bool ServerExists(Int64 id)
        {
            return _context.Servers.Any(e => e.ServerID == id);
        }
    }
}
