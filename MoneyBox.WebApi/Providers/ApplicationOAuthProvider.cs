using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using MoneyBox.WebApi.Models;
using MoneyBox.Business.IdentityManagers;
using MoneyBox.DAL;
using System.Data.Entity;
using MoneyBox.Domain.Models;

namespace MoneyBox.WebApi.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly string _publicClientId;

        public ApplicationOAuthProvider(string publicClientId)
        {
            if (publicClientId == null)
            {
                throw new ArgumentNullException("publicClientId");
            }

            _publicClientId = publicClientId;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userManager = context.OwinContext.GetUserManager<MoneyBoxUserManager>();

            string loginType = context.OwinContext.Get<string>("loginType");

            MoneyBoxUser user = null;

            using (var db = new MoneyBoxDb())
            {
                if (loginType == "phone")
                {
                    user = await db.Users.FirstOrDefaultAsync(u => u.PhoneNumber == context.UserName);
                }

                if (loginType == "email")
                {
                    user = await db.Users.FirstOrDefaultAsync(u => u.Email == context.UserName);
                }

                if (loginType == "userName")
                {
                    user = await db.Users.FirstOrDefaultAsync(u => u.UserName == context.UserName);
                }
            }

            if (user == null)
            {
                context.SetError("invalid_grant", "Kullanıcı adı yada parolanız yanlış.");
                return;
            }

            user = await userManager.FindAsync(user.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "Kullanıcı adı yada parolanız yanlış.");
                return;
            }

            ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager,
               OAuthDefaults.AuthenticationType);

            oAuthIdentity.AddClaim(new Claim("CompanyId", user.CompanyId.ToString()));

            ClaimsIdentity cookiesIdentity = await user.GenerateUserIdentityAsync(userManager,
                CookieAuthenticationDefaults.AuthenticationType);

            AuthenticationProperties properties = CreateProperties(user.UserName);
            AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
            context.Validated(ticket);
            context.Request.Context.Authentication.SignIn(cookiesIdentity);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            // Resource owner password credentials does not provide a client ID.
            if (context.ClientId == null)
            {
                string loginType = context.Parameters["id_Type"];
                context.OwinContext.Set<string>("loginType", loginType);
                context.Validated();
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            if (context.ClientId == _publicClientId)
            {
                Uri expectedRootUri = new Uri(context.Request.Uri, "/");

                if (expectedRootUri.AbsoluteUri == context.RedirectUri)
                {
                    context.Validated();
                }
            }

            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }
    }
}