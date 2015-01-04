namespace Shorter.Web.Api.App_Start
{
    using System;
    using System.Configuration;
    using System.Web.Http;

    using Microsoft.WindowsAzure.Storage;

    using Shorter.Providers;
    using Shorter.Providers.Azure;

    using SimpleInjector;
    using SimpleInjector.Integration.WebApi;

    public static class DependencyConfig
    {
        public static void Register(Container container)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["database"];
            if (connectionString == null)
            {
                throw new ApplicationException("No connection string");
            }

            container.RegisterWebApiControllers(GlobalConfiguration.Configuration);
            GlobalConfiguration.Configuration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);

            /* Comment or uncomment depending on prefered provider. */
            RegisterAzureProvider(container, connectionString.ConnectionString);
            // RegisterSqlProvider(container);
            // RegisterMySqlProvider(container);

            container.Verify();
        }

        private static void RegisterAzureProvider(Container container, string connectionString)
        {
            container.Register<IProvider, AzureProvider>();
            container.Register<ITableClient, TableClient>();
            container.RegisterSingle(() => CloudStorageAccount.Parse(connectionString));
        }
    }
}