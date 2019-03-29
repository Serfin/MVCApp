using System;
using System.Collections;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MVCApp.Infrastructure.Services;
using MVCApp.Presentation.Models;

namespace MVCApp.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService = new UserService();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task Register(RegisterViewModel model)
        {
            await _userService.RegisterAsync(Guid.NewGuid(), model.Email, model.Ign, model.Password, "User");

            RedirectToAction("Index");
        }

        public async Task<ActionResult> List()
        {
            var users = await _userService.GetAllAsync();
            return View(users);
        }
    }
}