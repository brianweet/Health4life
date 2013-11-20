using System.Web.Mvc;
using Health4life.Repositories;
using WebMatrix.WebData;

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

        public PartialViewResult Nav()
        {
            int? activityCount = null;

            var userId = WebSecurity.CurrentUserId;
            if(userId != -1)
                using (var repo = new ActivityRepository())
                {
                    activityCount = repo.CountNewByUserId(userId);
                }

             return PartialView("_NavBar", activityCount);
        }
    }
}
