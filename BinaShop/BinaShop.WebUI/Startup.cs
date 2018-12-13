using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BinaShop.WebUI.Startup))]
namespace BinaShop.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
