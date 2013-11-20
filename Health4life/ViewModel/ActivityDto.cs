using System;

namespace Health4life.ViewModel
{
    public class ActivityDto
    {
        public bool IsFromGp { get; set; }

        public bool IsNew { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set;  }
    }
}