using Creating_ViewComponent.Models;
using Microsoft.AspNetCore.Mvc;

namespace Creating_ViewComponent.ViewComponents
{
	public class GridViewComponent : ViewComponent
	{
		public async Task<IViewComponentResult> InvokeAsync( PersonGridModel grid)
		{
			return View(grid); //only calls partial views
			//by default you should make the view name is Default.cshtml
			//-->but what if you used another name?
			//it will not be recongized , so you should pass the name to the return View("viewName");
			//Views/Shared/Components/name_of_ViewComponentClass/viewname.cshtml
		}

	}
}
