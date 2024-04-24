using BookLibrary.Commands;
using BookLibrary.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.OData.Query;
using BookLibrary.Filters;

namespace BookLibrary.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ServiceFilter(typeof(JwtAuthorizationFilter))] 
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IBookRepository<Book> _repositoryBook;
        private readonly ILogger<BooksController> _logger;

        public BooksController(IMediator mediator, IBookRepository<Book> repositoryBook, ILogger<BooksController> logger)
        {
            _mediator = mediator;
            _repositoryBook = repositoryBook;
            _logger = logger;
        }

        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            try
            {
                var books = await _repositoryBook.GetAllAsync();
                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to retrieve books");
                return StatusCode(500, "An error occurred while retrieving books.");
            }
        }

        [HttpPost]

        public async Task<IActionResult> Post([FromBody] CreateBook command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var response = await _mediator.Send(command);
                return CreatedAtAction(nameof(Get), new { id = response.BookId }, response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create a book");
                return StatusCode(500, "An error occurred while creating the book.");
            }
        }
    }
}
