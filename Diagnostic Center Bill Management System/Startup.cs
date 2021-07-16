using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Diagnostic_Center_Bill_Management_System.Startup))]
namespace Diagnostic_Center_Bill_Management_System
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
