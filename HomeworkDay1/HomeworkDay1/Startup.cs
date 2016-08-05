using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HomeworkDay1.Startup))]
namespace HomeworkDay1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
