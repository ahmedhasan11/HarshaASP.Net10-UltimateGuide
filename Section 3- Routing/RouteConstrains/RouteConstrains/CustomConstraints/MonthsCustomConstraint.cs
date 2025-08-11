
using System.Text.RegularExpressions;

namespace RouteConstrains.CustomConstraints
{
	public class MonthsCustomConstraint : IRouteConstraint
	{
		//route-->the endpoint that will be executed
		//routeKey--> the parameter 
		//values-->contains the route values that recieved from incoming request
		public bool Match(HttpContext? httpContext, IRouter? route,	string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
		{
			Regex regex = new Regex("^(apr|jul|oct|jan)$");
			string monthValue = Convert.ToString(values[routeKey]);
			if (regex.IsMatch(monthValue))
			{
				return true;
			}
			return false;
		}
	}
}
