using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using BookLibrary.Entities;
using BookLibrary.Repositories;

namespace BookLibrary.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository<User> _userRepository;

        public UsersController(IUserRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("{id}")]
        [EnableQuery]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userRepository.GetUserById(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdUser = await _userRepository.AddUser(user);

            if (createdUser == null)
                return BadRequest();

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }
    }
}
