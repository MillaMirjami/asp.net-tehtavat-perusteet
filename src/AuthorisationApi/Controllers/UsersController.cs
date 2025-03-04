using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AuthorisationApi.Data; //refers to file where's the logic for the db connection
using Authorisation.Models; //file where's the model UserItem
using Authorisation.Data; //file where's the db's connection classes

namespace AuthorisationApi.Controllers;

[ApiController] // API-controller class, takes care of HTTP-requests
[Route("[controller]")] // Sets the route which directs the requests to this controller
    public class UsersController : ControllerBase // Sets UserController class that inherits ControllerBase class
    {
        private readonly UserContext _context; // Sets a private field that holds the db connection 
        public UsersController(UserContext context) //Constructor method which gets UserContext as a parameter
        {
            _context = context; // Sets _context field's value to the incoming UserContext
        }

        //Get /Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserItem>>> GetUsers() // Asynchronous method that returns all users
        {
            // To check if there's data in the db
            var users = await _context.Users.ToListAsync(); // Loads all the users from the db asynchronously

            // If db is empty, return basedata
            if(users.Count == 0)
            {
                users = UserData.GetInitialUsers(); // Gets initial user info
                _context.Users.AddRange(users); // Adds user to the db
                await _context.SaveChangesAsync(); // Saves the changes
            }
            return Ok(users); // Returns users as HTTP 200 OK -response
        }
        // GET: /Users/5
        [HttpGet("{id}")] // HTTP GET -request that takes id parameter from the route
        public async Task<ActionResult<UserItem>> GetUserItem(long id) // Asynchronous method that gets a single user
        {
            var userItem = await _context.Users.FindAsync(id); // Gets user from db by id

            if (userItem == null) // If there's no such user
            {
                return NotFound(); // Returns HTTP 404 Not Found -response
            }

            return userItem; // Returns the found user
        }

        // PUT: Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserItem(long id, UserItem userItem) // Asynchorous, updates user's data
        {
            if (id != userItem.Id) // If sent id does'n match the user id
            {
                return BadRequest(); // Returns HTTP 400 Bad Request -response
            }

            _context.Entry(userItem).State = EntityState.Modified; // Marks that the user's data can be modified

            try
            {
                await _context.SaveChangesAsync(); // Saves the changes to the db
            }
            catch (DbUpdateConcurrencyException) // If there's a conflict in the db's state
            {
                if (!UserItemExists(id)) // If the user doesn't exist in the db anymore
                {
                    return NotFound(); // Returns HTTP 404 Not Found -response
                }
                else
                {
                    throw; // Else throws the error forward
                }
            }

            return NoContent(); // Returns HTTP 204 No Content -response cause data is not being returned
        }

        // POST: /Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserItem>> PostUserItem(UserItem userItem) // Asynchronous method that creates new user
        {
            // Checks if username already exists
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == userItem.UserName);
            if(existingUser != null) {
                return Conflict("Username already exists!"); // If username already exists, returns HTTP 409 Conflict -response
            }
            
            _context.Users.Add(userItem); // Adds new user to the db
            await _context.SaveChangesAsync(); // Saves changes to the db

            // return CreatedAtAction("GetUserItem", new { id = userItem.Id }, userItem); Ei käytetä, koska sisältää hardcoding
            return CreatedAtAction(nameof(GetUserItem), new { id = userItem.Id }, userItem); // Returns HTTP 201 Created -response and the new user's info
        }

        // POST: /Users/Login
        [HttpPost("Login")]
        public async Task<ActionResult<UserItem>> Login ([FromBody] LoginModel loginModel) // Asynchronous method for logging in
        {
            var userItem = await _context.Users.FirstOrDefaultAsync(u => u.UserName == loginModel.UserName); // Gets the user from the db

            if(userItem == null || userItem.Password != loginModel.Password) // If there's no user or password is incorrect
            {
                return Unauthorized("Invalid username or password!"); // Returns HTTP 401 Unauthorized - response
            }
            //returns Ok -message, if success
            return Ok("Login successful!");
        }

        public class LoginModel // Defines LoginModel class which has username and password for logging in
        {
            public string UserName {get; set;}
            public string Password {get; set;}
        }

        // DELETE: /UserItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserItem(long id)
        {
            var userItem = await _context.Users.FindAsync(id); // Gets the user by id
            if (userItem == null) // If there's no such user
            {
                return NotFound(); // Returns HTTP 404 Not Found -response Not Found
            }

            _context.Users.Remove(userItem); // Removes the user
            await _context.SaveChangesAsync(); // Saves the changes

            return NoContent(); // Returns HTTP 204 No Content -response
        }

        private bool UserItemExists(long id) // Aid method to help check if the user exists in the db 
        {
            return _context.Users.Any(e => e.Id == id); // Returns true if users can be found, otherwise false
        }

}
