using Microsoft.AspNetCore.Mvc;
using Service;
using ServiceContracts;
using ServiceContracts.DTO;

namespace CRUD_Application.Controllers
{
    public class PersonsController : Controller
    {
       // private readonly ICountryService _countryservice;
        private readonly IPersonService _personservice;

		//// ICountryService countryService
		public PersonsController(IPersonService personService)      //if you supply an obj of the IPersonService-->itreturns a  obj of PersonService 
		{
            _personservice = personService;
            //_countryservice = countryService;
        }

        [Route("persons/index")]
        [Route("/")]
        public IActionResult Index()
        {
            List<PersonResponse> AllPersons = _personservice.GetAllPersons(); 
            return View(AllPersons);
        }
    }
}
