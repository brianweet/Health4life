using System.ComponentModel.DataAnnotations;
using Health4life.Models;

namespace Health4life.ViewModel
{
    public class ManageVm
    {
        public Gender? Gender { get; set; }

        [Range(0, 120)]
        public int? Age { get; set; }

        public LocalPasswordModel LocalPasswordModel { get; set; }
    }
}