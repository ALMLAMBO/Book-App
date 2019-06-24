using BookApp.Dtos;
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
    public class AuthorsController : Controller
    {
        private ReflectiveRepository<Author> repository;

        public AuthorsController(ReflectiveRepository<Author> repo)
        {
            repository = repo;
        }

        [HttpGet]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AuthorDto>))]
        public IActionResult GetAuthors()
        {
            List<Author> authors = repository.Get().ToList();
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            List<AuthorDto> authorDtos = new List<AuthorDto>();
            foreach (Author author in authors)
            {
                authorDtos.Add(new AuthorDto()
                {
                    Id = author.Id,
                    FirstName = author.FirstName,
                    LastName = author.LastName
                });
            }
            return Ok(authorDtos);
        }

        //api/countries/countryId
        [HttpGet("{authorId}", Name = "GetAuthor")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(AuthorDto))]
        public IActionResult GetAuthor(int authorId)
        {
            if (!repository.Exists(authorId))
            {
                return NotFound();
            }

            Author author = repository.GetById(authorId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            AuthorDto authorDto = new AuthorDto()
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName
            };

            return Ok(authorDto);
        }

        [HttpPost("")]
        [ProducesResponseType(201, Type = typeof(Author))]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult Create([FromBody]Author authorToCreate)
        {
            if (authorToCreate == null)
            {
                return BadRequest(ModelState);
            }

            Author author = repository.Get()
                .Where(c => (c.FirstName + c.LastName).Trim().ToUpper() == (authorToCreate.FirstName + authorToCreate.LastName).Trim().ToUpper())
                .FirstOrDefault();

            if (author != null)
            {
                ModelState
                    .AddModelError("", $"Author {author.FirstName + " " + author.LastName} already exists.");

                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!repository.Create(authorToCreate))
            {
                ModelState
                    .AddModelError("", $"Something went wrong while saving {authorToCreate.FirstName + " " + authorToCreate.LastName}.");

                return StatusCode(500, ModelState);
            }

            return CreatedAtRoute("GetAuthor",
                new { authorId = authorToCreate.Id },
                authorToCreate
            );
        }
    }
}
