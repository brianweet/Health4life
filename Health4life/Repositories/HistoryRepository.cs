using System;
using System.Collections.Generic;
using System.Linq;
using Health4life.Models;

namespace Health4life.Repositories
{
    public class HistoryRepository : IDisposable
    {
        private readonly H4LContext _context;

        public HistoryRepository()
        {
            _context = new H4LContext();
        }

        public List<HistoryEntry> GetByUserId(int userId)
        {
            var historyEntries = _context.HistoryEntries.Where(x => x.OwnerUserId == userId);
            return historyEntries.OrderByDescending(x => x.Date).ToList();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int Add(string description, string inputdata, DateTime date, int userId)
        {
            _context.HistoryEntries.Add(new HistoryEntry
                {
                    Description = description,
                    DomainDescription = inputdata,
                    Date = date,
                    OwnerUserId = userId
                });
            return _context.SaveChanges();
        }
    }
}