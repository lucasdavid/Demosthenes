using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Demosthenes.Startup))]
namespace Demosthenes
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
