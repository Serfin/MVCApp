using System.Threading.Tasks;
using System.Web.Mvc;
using MVCApp.Infrastructure.Services;

namespace MVCApp.Presentation.Controllers
{
    public class RotationsController : Controller
    {
        private readonly IRotationService _rotationService;

        public RotationsController(IRotationService rotationService)
        {
            _rotationService = rotationService;
        }

        // TODO : Add pagination
        public async Task<ActionResult> BrowseRotations(int page = 1)
        {
            var rotations = await _rotationService.GetAllAsync();
            return View(rotations);
        }
    }
}