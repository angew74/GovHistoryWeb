using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GovHistoryWeb.Startup))]
namespace GovHistoryWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
