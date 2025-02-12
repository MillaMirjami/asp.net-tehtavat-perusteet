using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuthorisationApi.Data;
using Authorisation.Models;

namespace AuthorisationApi.Controllers;

[ApiController]
[Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserContext _context;
        public UsersController(UserContext context)
        {
            _context = context;
        }

        //Get /Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserItem>>> GetUsers()
        {
            // To check if there's data in the db
            var users = await _context.Users.ToListAsync();

            // If db is empty, return basedata
            if(users.Count == 0)
            {
                users = UserData.GetInitialUsers();
                _context.Users.AddRange(users);
                await _context.SaveChangesAsync();
            }
            return Ok(users);
        }
        // GET: /Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserItem>> GetUserItem(long id)
        {
            var userItem = await _context.Users.FindAsync(id);

            if (userItem == null)
            {
                return NotFound();
            }

            return userItem;
        }

        // PUT: Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserItem(long id, UserItem userItem)
        {
            if (id != userItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(userItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserItemExists(id))
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

        // POST: /UserItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserItem>> PostTodoItem(UserItem userItem)
        {
            //if(userItem.UserName !== ) LUO TARKISTUS ETTÄ ONKO USERNAME JO KÄYTÖSSÄ
            _context.Users.Add(userItem);
            await _context.SaveChangesAsync();

            // return CreatedAtAction("GetUserItem", new { id = userItem.Id }, userItem); Ei käytetä, koska sisältää hardcoding
            return CreatedAtAction(nameof(GetUserItem), new { id = userItem.Id }, userItem);
        }

        // POST: /Users/Login
        [HttpPost("Login")]
        public async Task<ActionResult<UserItem>> Login ([FromBody] LoginModel loginModel)
        {
            var userItem = await _context.Users.FirstOrDefaultAsync(u => u.UserName == loginModel.UserName);

            if(userItem == null || userItem.Password != loginModel.Password)
            {
                return Unauthorized("Invalid username or password!");
            }
            //return users info, if success
            return Ok(userItem);
        }

        public class LoginModel
        {
            public string UserName {get; set;}
            public string Password {get; set;}
        }

        // DELETE: /UserItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserItem(long id)
        {
            var userItem = await _context.Users.FindAsync(id);
            if (userItem == null)
            {
                return NotFound();
            }

            _context.Users.Remove(userItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserItemExists(long id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

}
