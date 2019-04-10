using System.Web.Mvc;

namespace MVCApp.Presentation.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
    }
}