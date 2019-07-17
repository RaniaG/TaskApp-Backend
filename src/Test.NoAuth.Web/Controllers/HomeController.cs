using Microsoft.AspNetCore.Mvc;

namespace Test.NoAuth.Web.Controllers
{
    public class HomeController : NoAuthControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}