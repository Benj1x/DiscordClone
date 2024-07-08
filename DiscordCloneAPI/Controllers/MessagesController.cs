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
    public class MessagesController : ControllerBase
    {
        private readonly MessageContext _context;

        public MessagesController(MessageContext context)
        {
            _context = context;
        }

        // GET: api/Messages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetMessages()
        {
            return await _context.Messages.ToListAsync();
        }

        // GET: api/Messages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetMessage(string id)
        {
            var message = await _context.Messages.FindAsync(id);

            if (message == null)
            {
                return NotFound();
            }

            return message;
        }

        // PUT: api/Messages/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessage(string id, Message message)
        {
            if (id.Equals(message.MessageID))
            {
                return BadRequest();
            }

            _context.Entry(message).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessageExists(id))
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

        // POST: api/Messages
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Message>> PostMessage([FromForm]Message message)
        {
            message.MessageID = Guid.NewGuid().ToString("N");

            if (message.FormMessageAttachments != null) 
            {
                var memoryStream = new MemoryStream();
                message.MessageAttachments = new byte[message.FormMessageAttachments.Count()][];
                for (int i = 0; i < message.FormMessageAttachments.Count(); i++)
                {
                    await memoryStream.FlushAsync();
                    await message.FormMessageAttachments[i].CopyToAsync(memoryStream);
                    if (memoryStream.Length > 50000000)
                    {
                        return BadRequest($"Image {i} too large");
                    }
                    message.MessageAttachments[i] = new byte[memoryStream.Length];
                    message.MessageAttachments[i] = memoryStream.ToArray();
                }
                memoryStream.Dispose();
            }

            _context.Messages.Add(message);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                if (MessageExists(message.MessageID))
                {
                    while (MessageExists(message.MessageID))
                    {
                        _context.Messages.Remove(message);
                        message.MessageID = Guid.NewGuid().ToString("N");
                    }
                    _context.Messages.Add(message);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw;
                }
            }
            return CreatedAtAction(nameof(GetMessage), new { id = message.MessageID }, message);
        }

        // DELETE: api/Messages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(string id)
        {
            var message = await _context.Messages.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MessageExists(string id)
        {
            return _context.Messages.Any(e => e.MessageID.Equals(id));
        }
    }
}
