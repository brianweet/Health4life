using System;
using System.Collections.Generic;

namespace Health4life.ViewModel
{
    public class ConnectHubVm
    {
        public IEnumerable<ShareKey> ShareKeys { get; set; }
    }

    public class ShareKey
    {
        public Guid Id { get; set; }

        public bool IsActive {
            get { return ValidUntil > DateTime.Now; }
        }

        public DateTime ValidUntil { get; set; }

        public string ValidFor { get; set; }
    }
}