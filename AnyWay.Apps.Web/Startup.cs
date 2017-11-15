using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AnyWay.Apps.Web.Startup))]
namespace AnyWay.Apps.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
