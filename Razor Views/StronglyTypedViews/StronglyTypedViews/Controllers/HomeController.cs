using Microsoft.AspNetCore.Mvc;
using StronglyTypedViews.Models;

namespace StronglyTypedViews.Controllers
{
    public class HomeController : Controller
    {
		#region StronglyTypedViews
        /*used to access the modelobject or modelcollection in the view
         we use it when our view is tightly coupled with a specific model
        this means that look at our index.cshtml view , our view is tightly coupled
        with Person Class , so what we need to do is to bind view with this model class or model collection
        by using @model List<Person> because we need to bind it to a modelcollection
        then we can access our properties using the Model property*/

        //you have to supply the list of persons as argument in this View action method
		#endregion
		public IActionResult Index()
        {
			List<Person> people = new List<Person>()
	  {
		  new Person() { Name = "John", DateOfBirth = DateTime.Parse("2000-05-06"), PersonGender = Gender.Male},
		  new Person() { Name = "Linda", DateOfBirth = DateTime.Parse("2005-01-09"), PersonGender = Gender.Female},
		  new Person() { Name = "Susan", DateOfBirth = DateTime.Parse("2008-07-12"), PersonGender = Gender.Other}
	  };
			return View("Index",people); //View is predefined method that accepts the model object that is the List<Person> here
            //so now this people is returned or accessed by the View using the @model
        }

        public IActionResult Details(string personName)
        {
            List<Person> pepl = new List<Person>()    {
		  new Person() { Name = "John", DateOfBirth = DateTime.Parse("2000-05-06"), PersonGender = Gender.Male},
		  new Person() { Name = "Linda", DateOfBirth = DateTime.Parse("2005-01-09"), PersonGender = Gender.Female},
		  new Person() { Name = "Susan", DateOfBirth = DateTime.Parse("2008-07-12"), PersonGender = Gender.Other}
	  };

            foreach (var prsn in pepl)
            {
                if (personName == prsn.Name)
                {
                    return View("Details",prsn);
                }
            }


			return View();
        }
    }
}
