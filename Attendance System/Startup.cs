using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Attendance_System.Startup))]
namespace Attendance_System
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
