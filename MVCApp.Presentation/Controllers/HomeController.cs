using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using MVCApp.Infrastructure.Services;
using MVCApp.Infrastructure.ViewModels;
using MVCApp.Presentation.Models;

namespace MVCApp.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

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
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _userService.RegisterAsync(Guid.NewGuid(), model.Email, model.Ign, model.Password, "User");

                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    // Invalid data view
                }
            }

            return View();
        }

        public async Task<ActionResult> Details(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            return View(user);
        }

        public async Task<ActionResult> List()
        {
            var users = await _userService.GetAllAsync();
            return View(users);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            if (ModelState.IsValid)
            {
                await _userService.DeleteUserAsync(id);
                return RedirectToAction("List");
            }

            return View("List");
        }
    }
}