using System.Collections;
using System.Collections.Generic;
using Health4life.Models;

namespace Health4life.ViewModel
{
    public class UserAreaVm
    {
        public IEnumerable<ActivityDto> Activities { get; set; }
        public IEnumerable<ShareKeyDto> ShareKeys { get; set; }
        public IEnumerable<HistoryEntryDto> HistoryEntries { get; set; }
    }
}