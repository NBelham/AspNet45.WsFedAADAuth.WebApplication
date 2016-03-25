using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace WsFedAADAuth.dotnet45.WebApplication.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
#if DEBUG
			// This code block purely for debugging purposes
			if(User.Identity.IsAuthenticated)
			{
				var username = User.Identity.Name;
				ClaimsIdentity claimsIdentity = User.Identity as ClaimsIdentity;
				var displayName = claimsIdentity.Claims.FirstOrDefault(i => i.Type == "http://schemas.microsoft.com/identity/claims/displayname").Value;
			}
#endif
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Your contact page";

			return View();
		}

		public ActionResult Error(string message)
		{
			ViewBag.Message = message;
			return View("Error");
		}
	}
}