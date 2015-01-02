namespace Shorter.Web.Api
{
    using System.Web.Http;

    using Shorter.Web.Api.App_Start;

    using SimpleInjector;

    public class WebApiApplication : System.Web.HttpApplication
    {
        private readonly Container container = new Container();

        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            DependencyConfig.Register(this.container);
        }
    }
}