using IdentityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserService.Infrastructure.Entities;

namespace UserService.Infrastructure.Repositories.Users
{
    public class UserWithRoles
    {
        public UserWithRoles()
        {
            User = new User();
            Roles = new List<Role>();
        }
        public User User { get; set; }

        public List<Role> Roles { get; set; }

        public Claim[] ToClaims()
        {
            return new Claim[]
            {
                new Claim("user_id", User.UserId.ToString() ?? ""),
                new Claim(JwtClaimTypes.Name, (!string.IsNullOrEmpty(User.FirstName) && !string.IsNullOrEmpty(User.LastName)) ? (User.FirstName + " " + User.LastName) : ""),
                new Claim(JwtClaimTypes.GivenName, User.FirstName  ?? ""),
                new Claim(JwtClaimTypes.FamilyName, User.LastName  ?? ""),
                //new Claim(JwtClaimTypes.Email, user.Email  ?? ""),
                //new Claim("some_claim_you_want_to_see", user.Some_Data_From_User ?? ""),

                //roles
                new Claim(JwtClaimTypes.Role, String.Join(", ", Roles.Select(r => r.Name)))
            };
        }
    }
}
