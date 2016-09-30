using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjectHub.Startup))]
namespace ProjectHub
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
