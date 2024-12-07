using GenericApi.DataObjects;
using GenericApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace GenericApi.Controllers
{   

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(TokenProvider tokenProvider) : ControllerBase
    {
        // In-memory user storage
        private static readonly Dictionary<string, string> _users = [];

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            if (_users.ContainsKey(user.Username))
            {
                return BadRequest("User already exists");
            }

            _users[user.Username] = user.Password;
            return Ok("User registered successfully");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            if (!_users.TryGetValue(user.Username, out var storedPassword) || storedPassword != user.Password)
                return Unauthorized("Invalid credentials.");

            var token = tokenProvider.Create(user);

            return Ok(token);
        }
    }
}
