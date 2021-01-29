using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static Sprint_3_V1.Controllers.ManageController;

namespace Sprint_3_V1.App_Start
{
     public class IdentityConfig
        {
            public class AppLogInManager : SignInManager<ApplicationUser, string>
            {
                public AppLogInManager(ApplicationUserManager userManager, IAuthenticationManager authenticationManager)
                    : base(userManager, authenticationManager)
                {
                }
            }
        }
    }