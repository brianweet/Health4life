using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
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
            return View();
        }
    }
}
