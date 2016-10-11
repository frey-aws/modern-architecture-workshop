using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(sample_web_app.Startup))]
namespace sample_web_app
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
