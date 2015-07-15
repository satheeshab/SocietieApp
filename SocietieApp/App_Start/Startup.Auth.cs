using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.OAuth;
using Owin;
using SocietieApp.Providers;
using System;

namespace SocietieApp
{
    public partial class Startup
    {
        static Startup()
        {
            PublicClientId = "self";

            UserManagerFactory = () => new UserManager<IdentityUser>(new UserStore<IdentityUser>());

            OAuthOptions = new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/Token"),
                Provider = new ApplicationOAuthProvider(PublicClientId, UserManagerFactory),
                AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                AllowInsecureHttp = true
            };
        }

        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static Func<UserManager<IdentityUser>> UserManagerFactory { get; set; }

        public static string PublicClientId { get; private set; }

        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //    consumerKey: "",
            //    consumerSecret: "");

            var fbOptions = new FacebookAuthenticationOptions();
            fbOptions.AppId = "443769265795638";
            fbOptions.AppSecret = "9d1f61bf441a591906149a0abf21a4c4";
            fbOptions.Scope.Add("email");

            app.UseFacebookAuthentication(fbOptions);

            var googleOptions = new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "380736477376-37l3mjij7k6d0n59ksurj7le946qq958.apps.googleusercontent.com",
                ClientSecret = "U0DaoW3LfrgwBHHuOySfS8U4",
                CallbackPath = new PathString("/signin-google")
            };
            googleOptions.Scope.Add("email");

            app.UseGoogleAuthentication(googleOptions);
             
        }
    }
}
