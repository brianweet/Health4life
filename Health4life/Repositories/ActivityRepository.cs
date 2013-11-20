using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Health4life.Models;

namespace Health4life.Repositories
{
    public class ActivityRepository : IDisposable
    {
        private readonly H4LContext _context;

        public ActivityRepository()
        {
            _context = new H4LContext();
        }

        public List<Activity> GetByUserId(int userId)
        {
            var activities = _context.Activities.Where(x => x.OwnerUserId == userId);
            return activities.OrderByDescending(x => x.Date).ToList();
        }

        public int CountNewByUserId(int userId)
        {
            return _context.Activities.Count(x => x.OwnerUserId == userId && x.IsNew);
        }

        public List<Activity> GetNewByUserId(int userId)
        {
            var activities = _context.Activities.Where(x => x.OwnerUserId == userId && x.IsNew);
            return activities.OrderByDescending(x => x.Date).ToList();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}