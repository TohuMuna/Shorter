namespace Shorter.Web.Api.Controllers
{
    using System.Web.Http;

    using Shorter.Providers;

    public abstract class BaseController : ApiController
    {
        protected IProvider Provider { get; private set; }

        protected BaseController(IProvider provider)
        {
            this.Provider = provider;
        }
    }
}
