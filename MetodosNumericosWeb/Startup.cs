using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MetodosNumericosWeb.Startup))]
namespace MetodosNumericosWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
