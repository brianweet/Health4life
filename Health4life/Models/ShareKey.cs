using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Security;
using Health4life.ViewModel;

namespace Health4life.Models
{
    [Table("ShareKey")]
    public class ShareKey : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Key { get; set; }


        public DateTime ValidUntil { get; set; }
        public int? ValidForUserId { get; set; }

        public virtual MembershipUser ValidForUser
        {
            get
            {
                if(ValidForUserId != null)
                    return Membership.GetUser(ValidForUserId);
                return null;
            }
        }

        public ShareKeyDto ToShareKeyDto()
        {
            return new ShareKeyDto
                {
                    Key = this.Key,
                    ValidFor = (this.ValidForUserId.HasValue) ? "GP" : "Anyone",
                    ValidUntil = this.ValidUntil
                };
        }
    }
}