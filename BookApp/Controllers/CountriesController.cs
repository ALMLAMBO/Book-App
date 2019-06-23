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
		[HttpGet("{countryId}")]
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
	}
}
