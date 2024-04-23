using BookLibrary.Commands;
using BookLibrary.Filters;
using BookLibrary.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace BookLibrary.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ServiceFilter(typeof(JwtAuthorizationFilter))] // Apply the JwtAuthorizationFilter to this method
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IBookRepository<Book> _repositoryBook;


        public BooksController(IMediator mediator, IBookRepository<Book> repositoryBook)
        {
            this._mediator = mediator;
            this._repositoryBook = repositoryBook;


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
                return StatusCode(500, "An error occurred while retrieving data. "+ ex.Message);
            }
        }


        [HttpPost]
        [Route("Create")]

        public async Task<IActionResult> AddBook(CreateBook command)
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
