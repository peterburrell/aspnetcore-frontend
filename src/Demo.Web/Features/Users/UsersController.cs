using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Demo.Web.Features.Users
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var users = Enumerable.Range(1, 50).Select(i => new UserListItem
            {
                Id = i,
                FirstName = "First " + i,
                LastName = "Last" + i,
            }).ToList();

            return View(new UserListModel { Users = users });
        }

        [HttpGet("{userId:long}")]
        public IActionResult Details(long userId)
        {
            return View(UserModel.Get(userId));
        }

        [HttpGet("{userId:long}/edit")]
        public IActionResult Edit(long userId, string layout="vertical")
        {
            if(layout.Equals("horizontal", StringComparison.OrdinalIgnoreCase))
                return View("Horizontal", UserModel.Get(userId));

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