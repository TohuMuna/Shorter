namespace Shorter.Web.Api.Controllers
{
    using System.Web.Http;

    public class HomeController : ApiController
    {
        [Route("")]
        public IHttpActionResult Get()
        {
            return this.Ok();
        }
    }
}