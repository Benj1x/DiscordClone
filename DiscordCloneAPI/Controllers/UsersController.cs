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
    public class UsersController : ControllerBase
    {
        private readonly UserContext _context;

        public UsersController(UserContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            User user = await _context.Users.FirstAsync(user => user.UserID.Equals(id));

            if (user == null)
            {
                return NotFound();
            }

            //todo Set to null instead
            user.PhoneNumber = 0;
            user.Email = "";
            user.Password = "";

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, User user)
        {
            if (user.UserID.Equals(id))
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserIdExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        /*Users are stored under another long than the one in the it is saved with, this might be because of the RAM db, but it should be figured out*/
        public async Task<ActionResult<User>> PostUser(User user)
        {

            //Check if email already exists
            user.UserID = Guid.NewGuid().ToString("N");


            _context.Users.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                if (UserIdExists(user.UserID))
                {
                    while (UserIdExists(user.UserID))
                    {
                        _context.Users.Remove(user);
                        user.UserID = Guid.NewGuid().ToString("N");
                    }
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction(nameof(GetUser), new { id = user.UserID }, user);
        }

       //POST: api/Users
       [HttpPost("login")]
        public async Task<ActionResult<string>> LoginUser([FromBody] LoginInfo loginInfo)
        {
            var user = _context.Users.Where(u => u.Email.Equals(loginInfo.email)).First();

            if (user == null)
            {
                return NotFound();
            }

            if (!user.Password.Equals(loginInfo.password))
            {
                return NotFound();
            }

            //Here you'd probably want to do some 2FA

            //Placeholder--------------------------------
            //UserAuthToken auth = new();
            //-------------------------------------------
            string auth = $"Authenticated\n{user.UserID}";
            return auth;
        }

        [HttpPatch("{UserID}")]
        public async Task<IActionResult> PatchProfile(long UserID, [FromForm]User user)
        {
            if (user.UserID.Equals(UserID))
            {
                return BadRequest();
            }
            //--Read image
            var memoryStream = new MemoryStream();
            user.FormProfilePic.CopyToAsync(memoryStream);

            if (memoryStream.Length > 50000000)
            {
                return BadRequest("Image too large");
            }
            user.ProfilePic = memoryStream.ToArray();
            //---
            //Here you'd probably want to do some 2FA

            //Placeholder--------------------------------
            UserAuthToken auth = new UserAuthToken();
            //-------------------------------------------
            return Ok();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserIdExists(string id)
        {
            return _context.Users.Any(e => e.UserID.Equals(id));
        }
        
    }
}
