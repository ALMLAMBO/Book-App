using BookApp.Models;
using BookApp.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : Controller
    {
        private ReflectiveRepository<Book> repository;

        public BooksController(ReflectiveRepository<Book> repo)
        {
            repository = repo;
        }

        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Book>))]
        public IActionResult GetBooks()
        {
            List<Book> books = repository.Get().ToList();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(books);
        }

        //api/countries/countryId
        [HttpGet("{bookId}", Name = "GetBook")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(Book))]
        public IActionResult GetBook(int bookId)
        {
            if (!repository.Exists(bookId))
            {
                return NotFound();
            }

            Book book = repository.GetById(bookId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(book);
        }

        [HttpPost("")]
        [ProducesResponseType(201, Type = typeof(Book))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult Create([FromBody]Book bookToCreate)
        {
            if (bookToCreate == null)
            {
                return BadRequest(ModelState);
            }

            Book book = repository.Get()
                .Where(c => (c.Name).Trim().ToUpper() == (bookToCreate.Name).Trim().ToUpper())
                .FirstOrDefault();

            if (book != null)
            {
                ModelState
                    .AddModelError("", $"Author {book.Name} already exists.");

                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!repository.Create(bookToCreate))
            {
                ModelState
                    .AddModelError("", $"Something went wrong while saving {book.Name}.");

                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetBook",
                new { authorId = bookToCreate.Id },
                bookToCreate
            );
        }

        [HttpPut("{bookId}")]
        [ProducesResponseType(204)] //no content
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult Update(int bookId, [FromBody]Book updatedBook)
        {
            if (updatedBook == null || bookId != updatedBook.Id)
            {
                return BadRequest(ModelState);
            }
            if (!repository.Exists(bookId))
            {
                return NotFound();
            }
            if (!repository.Update(updatedBook))
            {
                ModelState.AddModelError("", $"Something went wrong updating {updatedBook.Name}");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("{bookId}")]
        [ProducesResponseType(204)] //no content
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult Delete(int bookId)
        {
            if (!repository.Exists(bookId))
            {
                return NotFound();
            }

            Book bookToDelete = repository
                .GetById(bookId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!repository.Delete(bookToDelete))
            {
                ModelState
                    .AddModelError("", $"Something went wrong deleting {bookToDelete.Name}");

                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
