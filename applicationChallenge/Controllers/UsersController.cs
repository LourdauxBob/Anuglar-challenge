using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using applicationChallenge.Data;
using applicationChallenge.Models;
using applicationChallenge.Services;
using Microsoft.AspNetCore.Authorization;

namespace applicationChallenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ShopContext _context;
        private IUserService _userService;

        public UsersController(ShopContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] User userParam)
        {
            var user = _userService.Authenticate(userParam.Email, userParam.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return await _context.Users
                .Include(o => o.Orders)
                .ThenInclude(ol => ol.OrderLines)
                .ThenInclude(p => p.Product)
                .ToListAsync();
        }

        [HttpGet("getIdUser/{name}")]
        public async Task<ActionResult<User>> getIdUser(string name)
        {
            return await _context.Users
                .Where(u => u.FirstName == name)
                .Include(o => o.Orders)
                .ThenInclude(ol => ol.OrderLines)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync();
        }


        // GET api/Users/ifAdmin
        //Function to get users if isAdmin == True
        [HttpGet("getAdmins")]
        public async Task<ActionResult<IEnumerable<User>>> GetAdmins()
            {
              return await _context.Users.Where(u => u.IsAdmin == true).ToListAsync();
            }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users
                .Include(o => o.Orders)
                .ThenInclude(ol => ol.OrderLines)
                .ThenInclude(p => p.Product)
                .SingleOrDefaultAsync(u => u.UserID == id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
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
                if (!UserExists(id))
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
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserID }, user);
        }

        // DELETE: api/Users/5

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
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

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserID == id);
        }
    }
}
