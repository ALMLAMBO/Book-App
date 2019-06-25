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
    public class CategoriesController : Controller
    {
        private ReflectiveRepository<Category> repository;

        public CategoriesController(ReflectiveRepository<Category> repo)
        {
            repository = repo;
        }

        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Category>))]
        public IActionResult GetCategories()
        {
            List<Category> books = repository.Get().ToList();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(books);
        }

        //api/countries/countryId
        [HttpGet("{categoryId}", Name = "GetCategory")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(Category))]
        public IActionResult GetCategory(int categoryId)
        {
            if (!repository.Exists(categoryId))
            {
                return NotFound();
            }

            Category book = repository.GetById(categoryId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(book);
        }

        [HttpPost("")]
        [ProducesResponseType(201, Type = typeof(Category))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult Create([FromBody]Category bookToCreate)
        {
            if (bookToCreate == null)
            {
                return BadRequest(ModelState);
            }

            Category book = repository.Get()
                .Where(c => (c.Name).Trim().ToUpper() == (bookToCreate.Name).Trim().ToUpper())
                .FirstOrDefault();

            if (book != null)
            {
                ModelState
                    .AddModelError("", $"Category {book.Name} already exists.");

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

            return CreatedAtRoute("GetCategory",
                new { categoryId = bookToCreate.Id },
                bookToCreate
            );
        }

        [HttpPut("{categoryId}")]
        [ProducesResponseType(204)] //no content
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult Update(int categoryId, [FromBody]Category updatedBook)
        {
            if (updatedBook == null || categoryId != updatedBook.Id)
            {
                return BadRequest(ModelState);
            }
            if (!repository.Exists(categoryId))
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

        [HttpDelete("{categoryId}")]
        [ProducesResponseType(204)] //no content
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult Delete(int categoryId)
        {
            if (!repository.Exists(categoryId))
            {
                return NotFound();
            }

            Category bookToDelete = repository
                .GetById(categoryId);

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
