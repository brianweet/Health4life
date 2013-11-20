using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Health4life.ViewModel;

namespace Health4life.Models
{
    [Table("HistoryEntry")]
    public class HistoryEntry : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public string DomainDescription { get; set; }

        public HistoryEntryDto ToHistoryEntryDto()
        {
            return new HistoryEntryDto
            {
                Description = this.Description,
                DomainDescription = this.DomainDescription,
                Date = this.Date
            };
        }
    }
}