using Microsoft.AspNetCore.Mvc;
using BookApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookApp.Models;
using BookApp.Dtos;

namespace BookApp.Controllers {
	[Route("api/[controller]")]
	[ApiController]
	public class CountriesController : Controller {
		private ICountryRepository _countryRepository;

		public CountriesController(ICountryRepository countryRepository) {
			_countryRepository = countryRepository;
		}

		//api/countries
		[HttpGet]
		[ProducesResponseType(400)]
		[ProducesResponseType(200, Type = typeof(IEnumerable<CountryDto>))]
		public IActionResult GetCountries() {
			List<Country> countries = _countryRepository
				.GetCountries().ToList();

			if(!ModelState.IsValid) {
				return BadRequest(ModelState);
			}

			List<CountryDto> countriesDto = new List<CountryDto>();

			foreach (Country country in countries) {
				countriesDto.Add(new CountryDto() {
					Id = country.Id, 
					Name = country.Name
				});
			}

			return Ok(countriesDto);
		}

		//api/countries/countryId
		[HttpGet("{countryId}", Name = "GetCountry")]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[ProducesResponseType(200, Type = typeof(CountryDto))]
		public IActionResult GetCountry(int countryId) {
			if(!_countryRepository.CountryExists(countryId)) {
				return NotFound();
			}

			Country country = _countryRepository.GetCountry(countryId);

			if(!ModelState.IsValid) {
				return BadRequest(ModelState);
			}

			CountryDto countryDto = new CountryDto() {
				Id = country.Id,
				Name = country.Name
			};

			return Ok(countryDto);
		}

		//api/countries/authors/authorId
		[HttpGet("authors/{authorId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[ProducesResponseType(200, Type = typeof(CountryDto))]
		public IActionResult GetCountryOfAnAuthor(int authorId) {

			Country country = _countryRepository
				.GetCountryOfAnAuthor(authorId);

			if(!ModelState.IsValid) {
				return BadRequest(ModelState);
			}

			CountryDto countryDto = new CountryDto() {
				Id = country.Id,
				Name = country.Name
			};

			return Ok(countryDto);
		}

		//api/countries/countryId/authors
		[HttpGet("{countryId}/authors")]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[ProducesResponseType(200, Type = typeof(IEnumerable<AuthorDto>))]
		public IActionResult GetAuthorFromACountry(int countryId) {
			if(!_countryRepository.CountryExists(countryId)) {
				return NotFound();
			}

			List<Author> authors = _countryRepository
				.GetAuthorsFromACountry(countryId)
				.ToList();

			if(!ModelState.IsValid) {
				return BadRequest(ModelState);
			}

			List<AuthorDto> authorsDto = new List<AuthorDto>();

			foreach (Author author in authors) {
				authorsDto.Add(new AuthorDto() {
					Id = author.Id,
					FirstName = author.FirstName,
					LastName = author.LastName
				});
			}

			return Ok(authorsDto);
		}

		[HttpPost]
		[ProducesResponseType(201, Type = typeof(Country))]
		[ProducesResponseType(400)]
		[ProducesResponseType(422)]
		[ProducesResponseType(500)]
		public IActionResult CreateCountry([FromBody]Country countryToCreate) {
			if(countryToCreate == null) {
				return BadRequest(ModelState);
			}

			Country country = _countryRepository.GetCountries()
				.Where(c => c.Name.Trim().ToUpper() == countryToCreate.Name.Trim().ToUpper())
				.FirstOrDefault();

			if(country != null) {
				ModelState
					.AddModelError("", $"Country {countryToCreate.Name} already exists.");

				return StatusCode(422, ModelState);
			}

			if(!ModelState.IsValid) {
				return BadRequest(ModelState);
			}

			if(!_countryRepository.CreateCountry(countryToCreate)) {
				ModelState
					.AddModelError("", $"Something went wrong while saving {countryToCreate.Name}.");

				return StatusCode(500, ModelState);
			}

			return CreatedAtRoute("GetCountry", 
				new { countryId = countryToCreate.Id },
				countryToCreate
			);
		}
	}
}
