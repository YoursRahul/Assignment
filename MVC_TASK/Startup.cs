using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_TASK.Startup))]
namespace MVC_TASK
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
