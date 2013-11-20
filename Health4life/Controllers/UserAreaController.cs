using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using System.Web.Profile;
using System.Web.Security;
using Health4life.Models;
using Health4life.Repositories;
using Health4life.ViewModel;
using WebMatrix.WebData;

namespace Health4life.Controllers
{
    [Authorize]
    public class UserAreaController : Controller
    {
        private int _userId;

        public UserAreaController()
        {
            _userId = WebSecurity.CurrentUserId;
        }

        public ActionResult Index()
        {
            var vm = new UserAreaVm();

            using (var repo = new ShareKeyRepository())
            {
                var shareKeys = repo.GetByUserId(_userId);
                vm.ShareKeys = shareKeys.Select(shareKey => shareKey.ToShareKeyDto());
            }

            using (var repo = new ActivityRepository())
            {
                var activities = repo.GetByUserId(_userId);
                vm.Activities = activities.Select(shareKey => shareKey.ToActivityDto());
            }

            using (var repo = new HistoryRepository())
            {
                var pHistory = repo.GetByUserId(_userId);
                vm.HistoryEntries = pHistory.Select(h => h.ToHistoryEntryDto());
            }

            return View(vm);
        }

        public ActionResult ConnectHub()
        {
            var vm = new ConnectHubVm();

            using (var repo = new ShareKeyRepository())
            {
                var shareKeys = repo.GetByUserId(_userId);
                vm.ShareKeys = shareKeys.Select(shareKey => shareKey.ToShareKeyDto());
            }

            return View(vm);
        }

        public ActionResult Activities()
        {
            var vm = new ActivitiesVm();

            using (var repo = new ActivityRepository())
            {
                var activities = repo.GetByUserId(_userId);
                vm.Activities = activities.Select(activity => activity.ToActivityDto());
            }

            return View(vm);
        }

        public ActionResult Diary()
        {
            return View();
        }

        public ActionResult PatientHistory()
        {
            var vm = new HistoryEntryVm();

            using (var repo = new HistoryRepository())
            {
                var pHistory = repo.GetByUserId(_userId);
                vm.HistoryEntries = pHistory.Select(h => h.ToHistoryEntryDto());
            }

            return View(vm);
        }

        public JsonResult AgendaEvents(string from, string to)
        {
            List<Activity> activities;
            using (var repo = new ActivityRepository())
            {
                activities = repo.GetByUserId(_userId);
            }

            List<HistoryEntry> historyEntries;
            using (var repo = new HistoryRepository())
            {
                historyEntries = repo.GetByUserId(_userId);
            }

            var act = activities.Select(a => new
                {
                    id = "a" + a.Id,
                    title = a.Description,
                    url = String.Format("Extra information for activity {0}", a.Description),
                    @class = "event-warning",
                    start = a.Date,
                    end = a.Date
                }).ToList();

            act.AddRange(historyEntries.Select(a => new
                {
                    id = "h" + a.Id,
                    title = a.Description,
                    url = String.Format("Extra information for {0}", a.Description),
                    @class = "event-info",
                    start = a.Date,
                    end = a.Date
                }));

            var obj = new
                {
                    success = 1,
                    result = act
                };

            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddPatientHistory(string description, string inputdata, string date, string askgp)
        {
            using (var repo = new HistoryRepository())
            {
                if (repo.Add(description, inputdata, DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture), _userId) == 1)
                {
                    ViewBag.Success = true;
                    ViewBag.Message = "New entry saved";
                }
                else
                {
                    ViewBag.Success = false;
                    ViewBag.Message = "Something went wrong, please contact us at info@ehs.nl";
                }
            }

            var vm = new HistoryEntryVm();

            using (var repo = new HistoryRepository())
            {
                var pHistory = repo.GetByUserId(_userId);
                vm.HistoryEntries = pHistory.Select(h => h.ToHistoryEntryDto());
            }

            return View("PatientHistory",vm);
        }

        [HttpPost]
        public ActionResult AddShareKey(string validfor, string validuntil)
        {
            using (var repo = new ShareKeyRepository())
            {
                DateTime until;
                if(validuntil.Equals("hour"))
                    until = DateTime.Now.AddHours(1);
                else
                    until = DateTime.Now.AddDays(1);

                var validFor = 0;
                if (validfor.Equals("gp"))
                {
                    using (var context = new H4LContext())
                    {
                        var userprofile = context.UserProfiles.Find(_userId);
                        if (userprofile != null && userprofile.GeneralPractitionerUserId.HasValue)
                            validFor = userprofile.GeneralPractitionerUserId.Value;
                    }
                }
                
                if (repo.Add(validFor, until, _userId) == 1)
                {
                    ViewBag.Success = true;
                    ViewBag.Message = "New entry saved";
                }
                else
                {
                    ViewBag.Success = false;
                    ViewBag.Message = "Something went wrong, please contact us at info@ehs.nl";
                }
            }

            var vm = new ConnectHubVm();

            using (var repo = new ShareKeyRepository())
            {
                var shareKeys = repo.GetByUserId(_userId);
                vm.ShareKeys = shareKeys.Select(shareKey => shareKey.ToShareKeyDto());
            }

            return View("ConnectHub",vm);
        }
    }
}
