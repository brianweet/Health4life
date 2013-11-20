using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Security;

namespace Health4life.Models
{
    [Table("UserProfile")]
    public class UserProfile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        
        public string Username { get; set; }
        
        public Gender? Gender { get; set; }
        
        [Range(0,120)]
        public int? Age { get; set; }

        public int? GeneralPractitionerUserId { get; set; }

        public virtual MembershipUser GeneralPractitionerUser
        {
            get
            {
                if(GeneralPractitionerUserId.HasValue)
                    return Membership.GetUser(GeneralPractitionerUserId);
                return null;
            }
        }
    }
}