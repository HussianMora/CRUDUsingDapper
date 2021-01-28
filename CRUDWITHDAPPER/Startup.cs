using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CRUDWITHDAPPER.Startup))]
namespace CRUDWITHDAPPER
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
