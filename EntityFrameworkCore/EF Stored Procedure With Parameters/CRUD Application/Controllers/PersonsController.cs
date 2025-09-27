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
			ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
			return View();
        }

		[Route("persons/create")]
		[HttpPost]

        
        public IActionResult Create(PersonAddRequest person)
        {
		_personservice.AddPerson(person);
          return RedirectToAction("Index");

		}

        [HttpGet]
        [Route("[action]/{personID}")]
        public IActionResult Edit(Guid personID)
        {
            PersonResponse? personresponse = _personservice.GetPersonByID(personID);
            if (personresponse==null)
            {
                return NotFound("id is invalid or null");
            }

			PersonUpdateRequest prsn = personresponse.ToPersonUpdateRequest();

            ViewBag.Countries = _countryservice.GetAllCountries().Select(temp => new SelectListItem() { Value = temp.CountryID.ToString() , Text = temp.Countryname });
            return View(prsn);
        }
        [HttpPost]
		[Route("[action]/{personID}")]
		public IActionResult Edit(PersonUpdateRequest personUpdateRequest)
        {
            
            PersonResponse response = _personservice.GetPersonByID(personUpdateRequest.PersonID);
            if (response==null)
            {
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
				PersonResponse updated = _personservice.UpdatePerson(personUpdateRequest);
				return RedirectToAction("Index");
			}
            else
            {
				ViewBag.Countries = _countryservice.GetAllCountries().Select(country => new SelectListItem() { Value = country.CountryID.ToString(), Text = country.Countryname });
				ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
				return View(response.ToPersonUpdateRequest());
            }
        }

        [HttpGet]
        [Route("{action}/{personID}")]
        public IActionResult Delete(Guid personID)
        {
			PersonResponse? person = _personservice.GetPersonByID(personID);
			if (person == null)
			{
                return RedirectToAction("Index");
			}
			return View(person);
        }


		[HttpPost]
		[Route("{action}/{personID}")]
		public IActionResult Delete(PersonResponse person)
		{
           PersonResponse? persnresponse= _personservice.GetPersonByID(person.PersonID);
            if(persnresponse==null)
            {
                return NotFound("id null or invalid");
            }

            _personservice.DeletePerson(person.PersonID);

            return RedirectToAction("Index");
		}

	}
}
