using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Poker.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Poker Hand Evaluator";

			return View();
		}

		public ActionResult Contact()
		{
			ViewBag.Message = "Travis Wise";

			return View();
		}
	}
}