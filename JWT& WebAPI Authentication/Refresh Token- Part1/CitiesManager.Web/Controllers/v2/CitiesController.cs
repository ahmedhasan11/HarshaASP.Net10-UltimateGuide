using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CitiesManager.Web.DatabaseContext;
using CitiesManager.Web.Models;

namespace CitiesManager.Web.Controllers.v2
{
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiController]
	[ApiVersion("2.0")]
	public class CitiesController : ControllerBase
    {
        private readonly CitiesDbContext _context;

        public CitiesController(CitiesDbContext context)
        {
            _context = context;
        } 

        /// <summary>
        /// Getting all Cities  Ordered By City Name
        /// </summary>
        /// <returns></returns>
        // GET: api/Cities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string?>>> GetCities()
        {
            return await _context.Cities.OrderBy(city=>city.CityName).Select(city=>city.CityName).ToListAsync();
        }

    }
}
