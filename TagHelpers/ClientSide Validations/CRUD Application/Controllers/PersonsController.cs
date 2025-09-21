using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Service;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;

namespace CRUD_Application.Controllers
{
    public class PersonsController : Controller
    {
       // private readonly ICountryService _countryservice;
        private readonly IPersonService _personservice;
        private readonly ICountryService _countryservice;

		//// ICountryService countryService
		public PersonsController(IPersonService personService,ICountryService countryservice)//if you supply an obj of the IPersonService-->itreturns a  obj of PersonService 
		{
            _personservice = personService;
            _countryservice = countryservice;
        }

        [Route("persons/index")]
        [Route("/")]
        public IActionResult Index(string searchBy, string? searchString, string sortBy="PersonName", SortOrderEnum sortOrder= SortOrderEnum.ASC)
        {
           //here we gave the SortBy and SortOrder Default Value
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

           List<PersonResponse> SortedList = _personservice.GetSortedPersons(FilteredList, sortBy,sortOrder );

            ViewBag.CurrentsortBy = sortBy;
            ViewBag.CurrentsortOrder = sortOrder.ToString();


			return View(SortedList);
        }

        [Route("persons/create")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Countries= _countryservice.GetAllCountries().Select(country=>new SelectListItem() { Value=country.CountryID.ToString(), Text=country.Countryname} );
            return View();
        }

		[Route("persons/create")]
		[HttpPost]

        public IActionResult Create(PersonAddRequest person)
        {
		_personservice.AddPerson(person);
          return RedirectToAction("Index");

		}
    }
}
