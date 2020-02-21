using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(S3.Train.WebPerFume.Startup))]
namespace S3.Train.WebPerFume
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
