namespace Shorter.Web.Api.Controllers
{
    using System;
    using System.Web.Http;

    [RoutePrefix("shorter")]
    public class ShorterController : BaseController
    {
        [Route("{shortCode}")]
        public IHttpActionResult GetUrl(string shortCode)
        {
            // Here we call our shorter service and locate an existing record
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult CreateShortCode()
        {
            // Add the url to our database and return the shortcode
            // Inside this call we will check if the url already exists
            throw new NotImplementedException();
        }
    }
}
