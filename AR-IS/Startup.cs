using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AR_IS.Startup))]
namespace AR_IS
{
    public partial class Startup
    {
            public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
