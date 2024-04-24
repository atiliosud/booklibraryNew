using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using BookLibrary.Entities;
using BookLibrary.Repositories;
using Microsoft.Extensions.Logging;

namespace BookLibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository<User> _userRepository;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserRepository<User> userRepository, ILogger<UsersController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        [HttpGet("{id}")]
        [EnableQuery]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var user = await _userRepository.GetUserById(id);

                if (user == null)
                {
                    _logger.LogWarning("User with ID {Id} not found.", id);
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving user with ID {Id}.", id);
                return StatusCode(500, "An error occurred while retrieving user data.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Attempt to create user failed due to invalid model state.");
                return BadRequest(ModelState);
            }

            try
            {
                var createdUser = await _userRepository.AddUser(user);

                if (createdUser == null)
                {
                    _logger.LogWarning("Failed to create user.");
                    return BadRequest("Failed to create user.");
                }

                return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user.");
                return StatusCode(500, "An error occurred while creating user.");
            }
        }
    }
}
