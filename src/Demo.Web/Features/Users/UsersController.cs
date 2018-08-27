using Microsoft.AspNetCore.Mvc;

namespace Demo.Web.Features.Users
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("{userId:long}")]
        public IActionResult Details(long userId)
        {
            return View(UserModel.Get(userId));
        }

        [HttpGet("{userId:long}/edit")]
        public IActionResult Edit(long userId)
        {
            return View(UserModel.Get(userId));
        }

        [HttpPost("{userId:long}/edit")]
        public IActionResult Edit(UserModel input)
        {
            if (!ModelState.IsValid)
                return View(input);

            return RedirectToAction("Index");
        }
    }
}