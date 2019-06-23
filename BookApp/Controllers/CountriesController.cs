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
		public ActionResult GetCountries() {
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

		[HttpGet("{countryId}")]
		[ProducesResponseType(400)]
		[ProducesResponseType(404)]
		[ProducesResponseType(200, Type = typeof(CountryDto))]
		public ActionResult GetCountry(int countryId) {
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
	}
}
