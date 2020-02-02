using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace AzureAuthDemo.Web
{
    [Route("/")]
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index()
        {
            return Ok("Home page");
        }

        [Authorize]
        [Route("/secret")]
        public IActionResult Secret()
        {
            var identity = ((ClaimsIdentity)HttpContext.User.Identity);
            var name = identity.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
            var email = identity.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name")?.Value;
            return new OkObjectResult(new { name, email });
        }
    }
}