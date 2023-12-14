using Microsoft.AspNetCore.Mvc;

namespace Diana.Areas.Manage.Controllers
{
	public class DashboardController : Controller
	{
		[Area("Manage")]
		public IActionResult Index()
		{
			return View();
		}
	}
}
