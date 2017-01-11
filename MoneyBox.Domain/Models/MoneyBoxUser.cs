using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace MoneyBox.Domain.Models
{
    public class MoneyBoxUser : IdentityUser
    {
        public DateTime? BirthDate { get; set; }
        public string FullName { get; set; }
        public int? CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<MoneyBoxUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }
    }
}
