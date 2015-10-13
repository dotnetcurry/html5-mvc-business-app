using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(A2_HTML5_Biz_App_New.Startup))]
namespace A2_HTML5_Biz_App_New
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
