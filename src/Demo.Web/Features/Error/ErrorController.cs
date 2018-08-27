using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Web.Features.Error
{
    [Route("[controller]")]
    public class ErrorController : Controller
    {
        [Route("")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorInfo { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
