using Owin;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(App.Schedule.WebApi.Startup))]
namespace App.Schedule.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
