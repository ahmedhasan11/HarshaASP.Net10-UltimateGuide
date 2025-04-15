using CustomModelBinder.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace CustomModelBinder.ModelBinderCustomClass
{
	public class ModelBinderProvider : IModelBinderProvider
	{
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

		//compiler will make all the actin method with Person parameter to model bind
		//with CustomModelProvider

		//and any action method that doesn't have the parameter of the Person type
		//will be implemeneted using the default model binding process

		//you have to add the provider as the main one in program.cs
		public IModelBinder? GetBinder(ModelBinderProviderContext context)
		{//we have to check if the ModelType is equal to Person class
		 //we can check this by context.Metadata
			if (context.Metadata.ModelType == typeof(Person))
			{
				return new BinderTypeModelBinder(typeof(PersonModelBinder));
			}
			//so after that you dont need to call the CustomModelBinding
			//before the action method parameter
			//you can now remove the sentence of [ModelBinder(BinderType=typeof(custommodelbinder))]
			//and every thing will work good
			
			throw new NotImplementedException();
		}
	}
}
