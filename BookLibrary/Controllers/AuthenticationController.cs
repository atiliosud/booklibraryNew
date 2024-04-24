using BookLibrary.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookLibrary.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(IMediator mediator, ILogger<AuthenticationController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpPost("SignIn")] // Simplified route, combined with HTTP method for clarity
        public async Task<IActionResult> SignIn(SignIn command)
        {
            try
            {
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during the sign-in process."); // Logging the error
                return StatusCode(500, "An error occurred during the sign-in process. Please try again later.");
            }
        }
    }
}
