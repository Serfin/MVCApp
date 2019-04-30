using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MVCApp.Common.ViewModels;
using MVCApp.Infrastructure.CommandHandlers;
using MVCApp.Infrastructure.Commands.User;
using MVCApp.Infrastructure.Interfaces;

namespace MVCApp.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _accountService;
        private readonly IUserService _userService;
        private readonly IRotationService _rotationService;
        private readonly ICommandHandler<RegisterUser> _commandHandler;

        public HomeController(IUserService accountService, IUserService userService, IRotationService rotationService,
            ICommandHandler<RegisterUser> commandHandler)
        {
            _accountService = accountService;
            _userService = userService;
            _rotationService = rotationService;
            _commandHandler = commandHandler;
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

        public ActionResult SignIn()
        {
            return View();
        }

        public ActionResult BrowseRotations()
        {
            return RedirectToAction("BrowseRotations", "Rotations");
        }

        public ActionResult Login()
        {
            return View();
        }

        public async Task<ActionResult> Details(Guid id)
        {
            var user = await _accountService.GetByIdAsync(id);
            return View(user);
        }

        public async Task<ActionResult> List()
        {
            var users = await _accountService.GetAllAsync();
            return View(users);
        }

        public async Task<ActionResult> Delete(Guid id)
        {
            var user = await _accountService.GetByIdAsync(id);
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SignIn(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var command = new RegisterUser()
                    {
                        Email = model.Email,
                        Ign = model.Ign,
                        Password = model.Password
                    };

                    await _commandHandler.HandleAsync(command);

                    return RedirectToAction("Index");
                }
                catch (Exception exception)
                {
                    // TODO : Add popup with error
                    ViewData.Add("Exception", exception.Message);
                    return View();
                }
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            if (ModelState.IsValid)
            {
                await _accountService.DeleteAccountAsync(id);
                return RedirectToAction("List");
            }

            return View("List");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _userService.LoginAsync(model.Email, model.Password);

                    HttpCookie cookie = new HttpCookie("User")
                    {
                        Expires = DateTime.Now.AddDays(1),
                        ["UserEmail"] = model.Email
                    };
                    Response.Cookies.Add(cookie);

                    return RedirectToAction("Index", "Account");
                }
                catch (Exception e)
                {
                    ViewData.Add("LoginException", e.Message);
                    return View();
                }
            }

            return View();
        }

        [HttpGet]
        public void Logout()
        {
            Request.Cookies.Clear();
            RedirectToAction("Index");
        }
    }
}