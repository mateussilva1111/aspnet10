using API.Data.Dto;
using API.Models;
using API.Services;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private IBooksServices _booksServices;
        private ILogger<BooksController> _logger;
        public BooksController(IBooksServices booksServices, ILogger<BooksController> logger)
        {
            _booksServices = booksServices;
            _logger = logger;
        }

        [HttpGet()]
        [ProducesResponseType(typeof(PersonDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation("Getting all books");
            var books = await _booksServices.GetAllAsync();
            if (books == null)
            {
                _logger.LogWarning("No books found");
                return NotFound();
            }
            return Ok(books);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Book), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation("Getting book with id {Id}", id);
            var book = await _booksServices.GetByIdAsync(id);
            if (book == null)
            {
                _logger.LogWarning("Book with id {Id} not found", id);
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Book), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Post([FromBody] Book book)
        {
            _logger.LogInformation("Creating a new book with title {Title}", book.Title);
            var entity = book.Adapt<BookDto>();
            var createdBook = await _booksServices.CreateAsync(entity);
            if (createdBook == null)
            {
                _logger.LogError("Failed to create book with title {Title}", book.Title);
                return BadRequest();
            }
            return Ok(createdBook);
        }

        [HttpPut()]
        [ProducesResponseType(typeof(Book), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Put([FromBody] Book book)
        {
            _logger.LogInformation("Updating book with id {Id}", book.Id);
            var entity = book.Adapt<BookDto>();
            var updatedBook = await _booksServices.UpdateAsync(entity);
            if (updatedBook == null)
            {
                _logger.LogError("Failed to update book with id {Id}", book.Id);
                return BadRequest();
            }
            return Ok(updatedBook);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Book), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public async Task<IActionResult> Delete(int id)
        {
            _logger.LogInformation("Deleting book with id {Id}", id);
            var result = await _booksServices.DeleteAsync(id);
            if (!result)
            {
                _logger.LogWarning("Book with id {Id} not found", id);
                return NotFound();
            }
            return Ok();
        }
    }
}
