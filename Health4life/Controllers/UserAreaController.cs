using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;

namespace Health4life.Controllers
{
    [Authorize]
    public class UserAreaController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ConnectHub()
        {
            return View();
        }

        public ActionResult Activities()
        {
            return View();
        }

        public ActionResult UserManagement()
        {
            return View();
        }

        public ActionResult Diary()
        {
            return View();
        }

        public ActionResult PatientHistory()
        {
            return View();
        }
    }
}
