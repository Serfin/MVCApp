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
    [Authorize]
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

        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [AllowAnonymous] // TODO : Temporary solution until will implementy identity mechanism
        public ActionResult BrowseRotations()
        {
            return RedirectToAction("BrowseRotations", "Rotations");
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

        [HttpGet]
        public async Task<ActionResult> Delete(Guid id)
        {
            var user = await _accountService.GetByIdAsync(id);
            return View(user);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
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

        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            try
            {
                await _userService.LoginAsync(model.Email, model.Password);
                var user = await _accountService.GetByEmailAsync(model.Email);

                HttpCookie cookie = new HttpCookie("BasicUserData");
                cookie["UserID"] = user.UserId.ToString();
                cookie["UserIGN"] = user.Ign;
                Response.Cookies.Add(cookie);

                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                // TODO : Do something with exception
                return View(model);
            }
        }

        [HttpGet]
        public void Logout()
        {
            Request.Cookies.Clear();
            RedirectToAction("Index");
        }
    }
}