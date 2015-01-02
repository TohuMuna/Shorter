namespace Shorter.Web.Api.App_Start
{
    using System.Web.Http;

    using Shorter.Providers;
    using Shorter.Providers.Azure;

    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;

    public static class DependencyConfig
    {
        public static void Register(Container container)
        {
            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            /* Comment or uncomment depending on prefered provider. */
            RegisterAzureProvider(container);
            // RegisterSqlProvider(container);
            // RegisterMySqlProvider(container);

            container.Verify();
        }

        private static void RegisterAzureProvider(Container container)
        {
            container.Register<IProvider, AzureProvider>();
            // Register table client
            // Register property resolver
        }
    }
}