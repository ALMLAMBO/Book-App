using Microsoft.AspNetCore.Mvc;
using BookApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
		public ActionResult GetCountries() {
			return Ok(_countryRepository
				.GetCountries().ToList()
			);
		}
	}
}
