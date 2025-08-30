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
        public IActionResult Index(string searchBy, string? searchString)
        {
           // List<PersonResponse> AllPersons = _personservice.GetAllPersons();

            //what this dict do--> it have actualName , displayName
            //Actual--> what is internally , Display--> what wil be shown
            //when the user selects the DisplayName-->interannaly will be taken as the ActualName
            ViewBag.SearchFields = new Dictionary<string, string>() 
            {
                //right Hand Side-->which wil be displayed to the user
                { nameof(PersonResponse.PersonName), "Person Name" },
				{ nameof(PersonResponse.EmailAddress), "Email" },
				{ nameof(PersonResponse.DateOfBirth), "Date Of Birth" },
				{ nameof(PersonResponse.Gender), "Gender" },
				{ nameof(PersonResponse.CountryID), "Country" },
				{ nameof(PersonResponse.Address), "Address" },
			};



           List<PersonResponse> FilteredList= _personservice.GetFilteredPersons(searchBy, searchString);
			ViewBag.CurrentsearchBy = searchBy;
			ViewBag.CurrentsearchString = searchString;
			return View(FilteredList);
        }
    }
}
