using Microsoft.AspNetCore.Mvc;
using ViewComponent_Assignment.Models;

namespace ViewComponent_Assignment.ViewComponents
{
	public class WeatherViewComponent:ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync(Citycomponent citycomponent)
		{
			return View(citycomponent);
		}
	}
}
