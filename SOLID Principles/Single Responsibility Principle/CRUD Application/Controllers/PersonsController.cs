using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rotativa.AspNetCore;
using Services;
using ServiceContracts;
using ServiceContracts.DTO;
using ServiceContracts.Enums;
using System.Threading.Tasks;
using RepositoryContracts;
using CRUD_Application.Filters.ActionFilters;
using Microsoft.VisualStudio.Web.CodeGeneration.CommandLine;
using CRUD_Application.Filters.ResultFilters;
using CRUD_Application.Filters.ResourceFilters;
using CRUD_Application.Filters.ExceptionFilters;

namespace CRUD_Application.Controllers
{
	[TypeFilter(typeof(ResponseHeaderActionFilter), Arguments = new object[] {"CUSTOM-KEY-from-controller","CUSTOM-VALUE-from-controller"})]
    [TypeFilter(typeof(HandleExceptionFilter))]
	public class PersonsController : Controller
    {
        // private readonly ICountryService _countryservice;
        private readonly IPersonService _personservice;
        private readonly ICountryService _countryservice;
        private readonly ILogger<PersonsController> _logger;

        //// ICountryService countryService
        public PersonsController(IPersonService personService, ICountryService countryservice,
            ICountriesRepository countriesRepository, IPersonsRepository personsRepository,
            ILogger<PersonsController> logger)//if you supply an obj of the IPersonService-->itreturns a  obj of PersonService 
        {
            _personservice = personService;
            _countryservice = countryservice;
            _logger = logger;
            //_countriesRepository = countriesRepository;
            //_personsRepository = personsRepository; 
        }

        [Route("persons/index")]
        [Route("/")]
        [TypeFilter(typeof(PersonsListActionFilter))]
        [TypeFilter(typeof(ResponseHeaderActionFilter), Arguments =new object[] {"CUSTOM-KEY-from-action","CUSTOM-VALUE-from-action"})]
        [TypeFilter(typeof(PersonsListResultFilter))]
        public async Task<IActionResult> Index(string searchBy, string? searchString, string sortBy="PersonName", SortOrderEnum sortOrder= SortOrderEnum.ASC)
        {
            _logger.LogDebug($"searchBy:{searchBy}, searchString:{searchString}, sortBy={sortBy}, sortOrder={sortOrder}");
            _logger.LogInformation("Index action method from PersonsController");
           //here we gave the SortBy and SortOrder Default Value
           // List<PersonResponse> AllPersons = _personservice.GetAllPersons();

            //what this dict do--> it have actualName , displayName
            //Actual--> what is internally , Display--> what wil be shown
            //when the user selects the DisplayName-->interannaly will be taken as the ActualName
   //         ViewBag.SearchFields = new Dictionary<string, string>() 
   //         {
   //             //right Hand Side-->which wil be displayed to the user
   //             { nameof(PersonResponse.PersonName), "Person Name" },
			//	{ nameof(PersonResponse.EmailAddress), "Email" },
			//	{ nameof(PersonResponse.DateOfBirth), "Date Of Birth" },
			//	{ nameof(PersonResponse.Gender), "Gender" },
			//	{ nameof(PersonResponse.CountryID), "Country" },
			//	{ nameof(PersonResponse.Address), "Address" },
			//};



           List<PersonResponse> FilteredList= await _personservice.GetFilteredPersons(searchBy, searchString);
			//ViewBag.CurrentsearchBy = searchBy;
			//ViewBag.CurrentsearchString = searchString;

           List<PersonResponse> SortedList = await _personservice.GetSortedPersons(FilteredList, sortBy,sortOrder );

            //ViewBag.CurrentsortBy = sortBy;
           // ViewBag.CurrentsortOrder = sortOrder.ToString();


			return View(SortedList);
        }

        [Route("persons/create")]
        [HttpGet]
		[TypeFilter(typeof(ResponseHeaderActionFilter), Arguments = new object[] { "custom-key", "custom-val" })]
		public async Task<IActionResult> Create()
        {
			List<CountryResponse> countries = await _countryservice.GetAllCountries();
			ViewBag.Countries= countries.Select(country=>new SelectListItem() { Value=country.CountryID.ToString(), Text=country.Countryname} );
			ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
			return View();
        }
		[Route("persons/create")]
		[HttpPost]
        [TypeFilter(typeof(PersonCreateEditPostActionFilter))]
        [TypeFilter(typeof(CreateDisableResourceFilter),Arguments =new object[] { false})]
        public async Task<IActionResult> Create(PersonAddRequest personRequest)
        {
			//if (!ModelState.IsValid)
			//{
			//	List<CountryResponse> countries = await _countryservice.GetAllCountries();
			//	ViewBag.Countries = countries.Select(temp =>
			//	new SelectListItem() { Text = temp.Countryname, Value = temp.CountryID.ToString() });

			//	ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
			//	return View(_countryservice);
			//}
			await _personservice.AddPerson(personRequest);
          return RedirectToAction("Index");

		}

        [HttpGet]
        [Route("[action]/{personID}")]
        public async Task<IActionResult> Edit(Guid personID)
        {
            PersonResponse? personresponse = await _personservice.GetPersonByID(personID);
            if (personresponse==null)
            {
                return NotFound("id is invalid or null");
            }

			PersonUpdateRequest prsn = personresponse.ToPersonUpdateRequest();

			List<CountryResponse> countries = await _countryservice.GetAllCountries();
			ViewBag.Countries = countries.Select(temp => new SelectListItem() { Value = temp.CountryID.ToString() , Text = temp.Countryname });
            return View(prsn);
        }
        [HttpPost]
		[Route("[action]/{personID}")]
		[TypeFilter(typeof(PersonCreateEditPostActionFilter))]
		public async Task<IActionResult> Edit(PersonUpdateRequest personRequest)
        {
            
            PersonResponse? response = await _personservice.GetPersonByID(personRequest.PersonID);
            if (response==null)
            {
                return RedirectToAction("Index");
            }
				PersonResponse updated =await _personservice.UpdatePerson(personRequest);
				return RedirectToAction("Index");

    //        else
    //        {
				//List<CountryResponse> countries = await _countryservice.GetAllCountries();
				//ViewBag.Countries = countries.Select(country => new SelectListItem() { Value = country.CountryID.ToString(), Text = country.Countryname });
				//ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
				//return View(response.ToPersonUpdateRequest());
    //        }
        }

        [HttpGet]
        [Route("{action}/{personID}")]
        public async Task<IActionResult> Delete(Guid personID)
        {
			PersonResponse? person = await _personservice.GetPersonByID(personID);
			if (person == null)
			{
                return RedirectToAction("Index");
			}
			return View(person);
        }


		[HttpPost]
		[Route("{action}/{personID}")]
		public async Task<IActionResult> Delete(PersonResponse person)
		{
           PersonResponse? persnresponse= await _personservice.GetPersonByID(person.PersonID);
            if(persnresponse==null)
            {
                return NotFound("id null or invalid");
            }

           await _personservice.DeletePerson(person.PersonID);

            return RedirectToAction("Index");
		}
        [Route("/PersonsPDF")]
        public async Task<IActionResult> PersonsPDF()
        {
            var persons = await _personservice.GetAllPersons();
            return new ViewAsPdf("PersonsPDF", persons, ViewData)
            {
				PageMargins = new Rotativa.AspNetCore.Options.Margins() { Top = 20, Right = 20, Bottom = 20, Left = 20 },
				PageOrientation = Rotativa.AspNetCore.Options.Orientation.Landscape
			};
        }

	}
}
