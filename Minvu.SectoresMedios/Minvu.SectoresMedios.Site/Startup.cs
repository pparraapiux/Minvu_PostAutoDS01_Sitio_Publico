using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Minvu.SectoresMedios.Site.Startup))]
namespace Minvu.SectoresMedios.Site
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
