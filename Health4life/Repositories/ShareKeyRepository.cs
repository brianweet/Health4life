using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Health4life.Models;

namespace Health4life.Repositories
{
    public class ShareKeyRepository : IDisposable
    {
        private readonly H4LContext _context;

        public ShareKeyRepository()
        {
            _context = new H4LContext();
        }

        public List<ShareKey> GetByUserId(int userId)
        {
            var shareKeys = _context.ShareKeys.Where(x => x.OwnerUserId == userId);
            return shareKeys.OrderByDescending(x => x.ValidUntil).ToList();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}