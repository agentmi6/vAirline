using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(vAirline.Startup))]
namespace vAirline
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
