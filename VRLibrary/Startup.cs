using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VRLibrary.Startup))]
namespace VRLibrary
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
