using System;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Web.Features.Home
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Route("About")]
        public IActionResult About()
        {
            throw new Exception("that route no longer exists");
        }
    }
}
