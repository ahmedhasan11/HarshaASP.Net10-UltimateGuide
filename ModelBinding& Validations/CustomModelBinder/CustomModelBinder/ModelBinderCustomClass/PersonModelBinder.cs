using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;
using CustomModelBinder.Models;

namespace CustomModelBinder.ModelBinderCustomClass
{
	public class PersonModelBinder : IModelBinder
	{
		#region What is CustomModelBinder
		//we use CustomModelBinder when there is a complex logic we want to do
		//in the levelof modelbinding , so you are making it
		//instead of using the default modelbinding process
		//things like user will enter first name and lastname and you want 
		//them to be grouped into Name -->we need here CustomModelBinder 
		#endregion

		#region How To Use CustomModelBinder
		//to use it ,1-inherit the calss from IModelBinder Class
		//2-implement the interface which is the method inthe IModelBinder Class
		//3-it's being used in the Controller in the parameter
		//public IActionResult Index([ModelBinder(BinderType=typeof(typehere the CustomModelBinder
		//class name thatyou genereated))] Person person) 
		#endregion

		#region steps of processing ModelBinding in back
		//how CustomModelBinder is used in the back? how code use it
		//1-as soon requets is recieved,
		//it invokes our CutsomModelBinder instead of the default modelbinding process
		//2-Read the Values from the ValueProvider
		//3-Create a ModelObject and return an Object of the Person Class
		//4- so the object thatyoucreatedin the Person class , will be recieved
		//here into the person parameter 
		#endregion

		#region ModelBinderProvider
		/*Suppose you want to use the CustomModelBinder Class for all the action methods
		 wherever theparticular type of model class is used(Person person) as 
		action method parameter, so you can decalre	the CustomModelBinder globally 
		by using the ModelBinderProvider , so any action method that has a parameter of
		Person type will have the CustomModelBinder , so if you don't need to
		write the sentence of [ModelBinder(BinderType=typeof(custommodelbinder))] every 
		time you want to use it
		*/
		#endregion

		public Task BindModelAsync(ModelBindingContext bindingContext)
		{
			Person person = new Person();
			//first of all we need to read the actual values of firstname , lastname
			//we can do this using the ValueProvider
			//here we read if there is a parameter called FirstName in the request
			//if it is found , get the value of it
			//and last thing , it returns object of ValueProviderResult
			if (bindingContext.ValueProvider.GetValue("FirstName").Length > 0)
			{
				//we used the FirstValue because may be the request have duplicate values for FirstName
				//so we are getting only the firstvalue of it
				//note: we will assign this into a model object , after that this
				//model object will be returned as a person object in the controller parameter
				person.PersonName=bindingContext.ValueProvider.GetValue("FirstName").FirstValue;
				if (bindingContext.ValueProvider.GetValue("LastName").Length > 0)
				{
					person.PersonName +=" "+ bindingContext.ValueProvider.GetValue("LastName").FirstValue;
				}
			};
			//now our modelbinding process have been finished
			//we have to provide the result of the BindingContext
			bindingContext.Result = ModelBindingResult.Success(person);
			//the person object here that you are sending as an argument
			//is being receieved in the Controller in the action method parameter

			return Task.CompletedTask;
		}
	}
}
