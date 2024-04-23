using BookLibrary.Commands;
using BookLibrary.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace BookLibrary.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksCommandsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BooksCommandsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<Book>> CreateBook(CreateBook request)
        {
            try
            {
                var book = await _mediator.Send(request);
                return Ok(book);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

    }

}
