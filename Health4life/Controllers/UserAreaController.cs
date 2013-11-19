using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using Health4life.ViewModel;

namespace Health4life.Controllers
{
    [Authorize]
    public class UserAreaController : Controller
    {
        public ActionResult Index()
        {
            var vm = new ConnectHubVm();
            vm.ShareKeys = new List<ShareKey>
                {
                    new ShareKey()
                        {
                            Id = Guid.NewGuid(),
                            ValidFor = "GP",
                            ValidUntil = new DateTime(2013, 11, 21, 20, 00, 00)
                        }
                    ,new ShareKey()
                        {
                            Id = Guid.NewGuid(),
                            ValidFor = "GP",
                            ValidUntil = new DateTime(2013, 11, 11, 10, 00, 00)
                        }
                };

            return View(vm);
        }

        public ActionResult ConnectHub()
        {
            var vm = new ConnectHubVm();
            vm.ShareKeys = new List<ShareKey>
                {
                    new ShareKey()
                        {
                            Id = Guid.NewGuid(),
                            ValidFor = "GP",
                            ValidUntil = new DateTime(2013, 11, 21, 20, 00, 00)
                        }
                    ,new ShareKey()
                        {
                            Id = Guid.NewGuid(),
                            ValidFor = "GP",
                            ValidUntil = new DateTime(2013, 11, 11, 10, 00, 00)
                        }
                };

            return View(vm);
        }

        public ActionResult Activities()
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
