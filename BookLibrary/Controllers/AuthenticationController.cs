using BookLibrary.Commands;
using BookLibrary.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace BookLibrary.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;


        public AuthenticationController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPost]
        [Route("SignIn")]
        public async Task<IActionResult> SignIn(SignIn command)
        {
            try
            {
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving data." + ex.Message);
            }
        }
    }
}
