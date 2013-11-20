using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Security;
using Health4life.Models;
using WebMatrix.WebData;

namespace Health4life
{
    public class InitDb : DropCreateDatabaseIfModelChanges<H4LContext>
    {
        private readonly SimpleRoleProvider _roleProvider = (SimpleRoleProvider)Roles.Provider;
        private readonly SimpleMembershipProvider _membershipProvider = (SimpleMembershipProvider)Membership.Provider;
        private H4LContext _context;

        protected override void Seed(H4LContext context)
        {
            _context = context;

            WebSecurity.InitializeDatabaseConnection("DefaultConnection",
               "UserProfile", "UserId", "Username", autoCreateTables: true);

            SeedAdmin();

            SeedTestMember();
        }
        private void SeedAdmin()
        {
            if (!_roleProvider.RoleExists("Admin"))
            {
                _roleProvider.CreateRole("Admin");
            }
            if (_membershipProvider.GetUser("admin", false) == null)
            {
                _membershipProvider.CreateUserAndAccount("admin", "adminadmin");
            }
            if (!_roleProvider.GetRolesForUser("admin").Contains("Admin"))
            {
                _roleProvider.AddUsersToRoles(new[] { "admin" }, new[] { "Admin" });
            }

            _context.Activities.Add(new Activity()
                {
                    Date = new DateTime(2013, 11, 11, 12, 00, 00),
                    Description = "Run 5 km",
                    IsFromGp = true,
                    IsNew = false
                });
            _context.Activities.Add(new Activity()
            {
                Date = new DateTime(2013, 11, 15, 12, 00, 00),
                Description = "Ride 10 km by bike",
                IsFromGp = false,
                IsNew = true
            });
        }

        private void SeedTestMember()
        {
            var generalPractitioner = _membershipProvider.GetUser("Armand Needles", false);
            int? gpUserId = (generalPractitioner != null) ? (int?)generalPractitioner.ProviderUserKey : null;

            if (!_roleProvider.RoleExists("Member"))
            {
                _roleProvider.CreateRole("Member");
            }
            if (_membershipProvider.GetUser("test", false) == null)
            {
                _membershipProvider.CreateUserAndAccount("test", "testtest", false, new Dictionary<string, object>() { { "GeneralPractitionerUserId", gpUserId } });
            }
            if (!_roleProvider.GetRolesForUser("test").Contains("Member"))
            {
                _roleProvider.AddUsersToRoles(new[] { "test" }, new[] { "Member" });
            }

            var testUser = _membershipProvider.GetUser("test", false);
            
            if(testUser == null)
                return;

            _context.Activities.Add(new Activity()
            {
                Date = new DateTime(2013, 11, 12, 12, 00, 00),
                Description = "Run 15 km",
                IsFromGp = true,
                IsNew = false
            });
            _context.Activities.Add(new Activity()
            {
                Date = new DateTime(2013, 11, 22, 12, 00, 00),
                Description = "Run 5 km",
                IsFromGp = false,
                IsNew = true
            });
            
            _context.ShareKeys.Add(new ShareKey()
                {
                    ValidUntil = new DateTime(2013, 11, 11, 12, 00, 00),
                    OwnerUserId = ((int)testUser.ProviderUserKey),
                    ValidForUserId = gpUserId
                });

            _context.ShareKeys.Add(new ShareKey()
            {
                ValidUntil = new DateTime(2013, 11, 22, 12, 00, 00),
                OwnerUserId = ((int)testUser.ProviderUserKey),
                ValidForUserId = gpUserId
            });

        }
    }
}