using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BlueSignal.Startup))]
namespace BlueSignal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
