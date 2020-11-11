using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FaktureStavkeApp.Startup))]
namespace FaktureStavkeApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
