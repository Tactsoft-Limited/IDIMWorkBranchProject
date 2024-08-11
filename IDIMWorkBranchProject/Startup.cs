using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IDIMWorkBranchProject.Startup))]
namespace IDIMWorkBranchProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
