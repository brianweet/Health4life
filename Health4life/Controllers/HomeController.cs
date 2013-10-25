using System.Web.Mvc;

namespace Health4life.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Your way to a better life.";

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
    }
}
