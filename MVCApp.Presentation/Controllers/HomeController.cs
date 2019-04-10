using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web.Mvc;
using MVCApp.Infrastructure.CommandHandlers;
using MVCApp.Infrastructure.Commands.User;
using MVCApp.Infrastructure.Services;
using MVCApp.Presentation.Models;

namespace MVCApp.Presentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRotationService _rotationService;
        private readonly ICommandHandler<RegisterUser> _commandHandler;

        public HomeController(IUserService userService, IRotationService rotationService,
            ICommandHandler<RegisterUser> commandHandler)
        {
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

        // TODO : Add pagination
        public async Task<ActionResult> BrowseRotations(int page = 1)
        {
            var rotations = await _rotationService.GetAllAsync();
            return View(rotations);
        }

        public ActionResult Login()
        {
            return View();
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
                    Session["LoggedUser"] = model.Email;
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
    }
}