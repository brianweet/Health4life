using System;

namespace Health4life.ViewModel
{
    public class ShareKeyDto
    {
        public Guid Key { get; set; }

        public bool IsActive {
            get { return ValidUntil > DateTime.Now; }
        }

        public DateTime ValidUntil { get; set; }

        public string ValidFor { get; set; }
    }
}