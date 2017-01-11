using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using MoneyBox.DAL;
using MoneyBox.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyBox.Business.IdentityManagers
{

    public class MoneyBoxUserManager : UserManager<MoneyBoxUser>
    {
        public MoneyBoxUserManager(IUserStore<MoneyBoxUser> store)
            : base(store)
        {
        }

        public static MoneyBoxUserManager Create(IdentityFactoryOptions<MoneyBoxUserManager> options, IOwinContext context)
        {
            context.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
            var manager = new MoneyBoxUserManager(new UserStore<MoneyBoxUser>(context.Get<MoneyBoxDb>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<MoneyBoxUser>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = false
            };
            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 3,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider = new DataProtectorTokenProvider<MoneyBoxUser>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
}
