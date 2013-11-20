using System.Web.Security;

namespace Health4life.Models
{
    public class BaseEntity
    {
        public int OwnerUserId { get; set; }

        public virtual MembershipUser OwnerUser
        {
            get { return Membership.GetUser(OwnerUserId); }
        }
    }
}