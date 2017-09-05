using System.Web;
using System.Web.Mvc;
using WebApplication4.Controllers;

namespace WebApplication4
{
	public class FilterConfig
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
			//filters.Add(new BasicAuthenticationAttribute());

		}
	}
}
