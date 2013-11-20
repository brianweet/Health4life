using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Health4life.ViewModel;

namespace Health4life.Models
{
    [Table("Activity")]
    public class Activity : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public bool IsFromGp { get; set; }

        public bool IsNew { get; set; }

        public ActivityDto ToActivityDto()
        {
            return new ActivityDto
            {
                IsFromGp = this.IsFromGp,
                Description = this.Description,
                Date = this.Date
            };
        }
    }
}