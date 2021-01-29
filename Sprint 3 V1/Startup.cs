using Microsoft.Owin;
using Owin;
using static Sprint_3_V1.Controllers.ManageController;

[assembly: OwinStartupAttribute(typeof(Sprint_3_V1.Startup))]
namespace Sprint_3_V1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
