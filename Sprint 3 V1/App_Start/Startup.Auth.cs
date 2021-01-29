using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sprint_3_V1
{
	public partial class Startup
	{        
		// For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
		public void ConfigureAuth(IAppBuilder app)
		{
			app.UseCookieAuthentication(new CookieAuthenticationOptions
			{
				AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
				LoginPath = new PathString("/Accounts/Login"),
				LogoutPath = new PathString("/Accounts/LogOff"),
				SlidingExpiration = true,
				ExpireTimeSpan = TimeSpan.FromHours(5)
			});
			app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
		}
	}
}