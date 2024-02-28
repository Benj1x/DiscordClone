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
        public async Task<ActionResult<User>> GetUser(long id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(long id, User user)
        {
            if (id != user.UserID)
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

            Random random = new Random();

            random.NextInt64(0, 9223372036854775807);
            user.UserID = random.NextInt64(0, 9223372036854775807) + 1;


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
                        user.UserID = random.NextInt64(0, 9223372036854775807) + 1;
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

        // POST: api/Users
        //[HttpPost]
        //public async Task<ActionResult<UserAuthToken>> LoginUser(string Email, string Password)
        //{
        //    var user = await _context.Users.FindAsync(Email);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    if (!user.Password.Equals(Password))
        //    {
        //        return NotFound();
        //    }

        //    //Here you'd probably want to do some 2FA

        //    //Placeholder--------------------------------
        //    UserAuthToken auth = new UserAuthToken();
        //    //-------------------------------------------
        //    return auth;
        //}

        [HttpPatch("{UserID}")]
        public async Task<IActionResult> PatchProfile(long UserID, [FromForm]User user)
        {
            if (UserID != user.UserID)
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

        private bool UserIdExists(long id)
        {
            return _context.Users.Any(e => e.UserID == id);
        }
        
    }
}
