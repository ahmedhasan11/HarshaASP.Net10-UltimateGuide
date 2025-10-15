using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CitiesManager.Web.DatabaseContext;
using CitiesManager.Web.Models;

namespace CitiesManager.Web.Controllers.v1
{
	[Route("api/v{version:apiVersion}/[controller]")]
	[ApiController]
	[ApiVersion("1.0")]
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
        public async Task<ActionResult<IEnumerable<City>>> GetCities()
        {
            return await _context.Cities.OrderBy(city=>city.CityName).ToListAsync();
        }

        // GET: api/Cities/5
        [HttpGet("{cityID}")]
        //this equals to --> [HttpGet] + [Route("{cityID}")] 
        public async Task<ActionResult<City>> GetCity(Guid cityID)
        {
            var city = await _context.Cities.FindAsync(cityID);//-->FindAsync==FirstOrDefaultAsync==Where(query).FirstOrDefaultAsync()

            if (city == null)
            {
                return NotFound();//-->==Response.StatusCode=404;
            }

            return city;
        }

        // PUT: api/Cities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
		//[Bind] is used tospecifywhichattributes you wanttobe binded
		//itis recommendedbecausemost of the casesyoudont need all properties
		//also  To protect from overposting attacks
		public async Task<IActionResult> PutCity(Guid id,[Bind(nameof(City.CityID),nameof(City.CityName))] City city)
        {
            if (id != city.CityID)
            {
                return BadRequest();
            }

            _context.Entry(city).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)//-->you will get this exception ifthe city object is updated by another user
            {
                if (!CityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();//-->empty response but along with the status code HTTP 200
        }

        // POST: api/Cities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<City>> PostCity([Bind(nameof(City.CityID),nameof(City.CityName))] City city)
        {
            _context.Cities.Add(city);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCity", new { cityID = city.CityID }, city);
            //returns a JSON object that contains details ofthe newly inserted city obj

        }

        // DELETE: api/Cities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCity(Guid id)
        {
            var city = await _context.Cities.FindAsync(id);
            if (city == null)
            {
                return NotFound();
            }

            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CityExists(Guid id)
        {
            return _context.Cities.Any(e => e.CityID == id);
        }
    }
}
