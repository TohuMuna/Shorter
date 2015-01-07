namespace Shorter.Web.Api.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Shorter.Core.Extensions;
    using Shorter.Providers;
    using Shorter.Web.Api.Models;

    [RoutePrefix("shorter")]
    public class ShorterController : BaseController
    {
        public ShorterController(IProvider provider)
            : base(provider)
        {
        }

        [Route("{shortCode}")]
        public async Task<IHttpActionResult> GetUrl(string shortCode)
        {
            var url = await this.Provider.GetUrlFromShortCode(shortCode);
            if (url == null)
            {
                return this.NotFound();
            }

            return this.Ok(url);
        }

        [Route]
        public async Task<IHttpActionResult> PostShortCode(CreateCodeViewModel request)
        {
            try
            {
                request.Url = request.Url.ValidateUrl();
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }

            var shortCode = await this.Provider.GenerateShortCode(request.Url);
            if (shortCode == null)
            {
                return this.Conflict();
            }

            return this.Ok(shortCode);
        }
    }
}
