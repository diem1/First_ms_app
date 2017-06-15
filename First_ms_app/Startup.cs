using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(First_ms_app.Startup))]
namespace First_ms_app
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
