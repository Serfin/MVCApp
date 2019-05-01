using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using MVCApp.Infrastructure.Interfaces;

namespace MVCApp.Presentation.Controllers
{
    public class RotationsController : Controller
    {
        private readonly IRotationService _rotationService;
        private int CurrentPage { get; set; }

        public RotationsController(IRotationService rotationService)
        {
            _rotationService = rotationService;
        }

        public async Task<ActionResult> ButtonPrevious()
        {
            if (CurrentPage > 1)
            {
                var rotations = await _rotationService.GetPageAsync(CurrentPage - 1);
                return View("BrowseRotations", rotations);
            }

            return View("BrowseRotations");
        }

        public async Task<ActionResult> ButtonNext()
        {
            var rotations = await _rotationService.GetPageAsync(CurrentPage + 1);
            return View("BrowseRotations", rotations);
        }
        
        public async Task<ActionResult> BrowseRotations(int page = 1)
        {
            CurrentPage = page;
            var rotations = await _rotationService.GetPageAsync(page);
            return View(rotations);
        }

        // TODO : Add validation, authorization
        public async Task JoinRotation(Guid userId, Guid rotationId)
            => await _rotationService.JoinRotationAsync(userId, rotationId);

        public async Task LeaveRotation(Guid userId, Guid rotationId)
            => await _rotationService.LeaveRotationAsync(userId, rotationId);

        public async Task DeleteRotation(Guid rotationId)
            => await _rotationService.DeleteRotation(rotationId);
    }
}