using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using MVCApp.Common.ViewModels;
using MVCApp.Infrastructure.Interfaces;

namespace MVCApp.Presentation.Controllers
{
    //[Authorize]
    public class RotationsController : Controller
    {
        private readonly IRotationService _rotationService;

        public RotationsController(IRotationService rotationService)
        {
            _rotationService = rotationService;
        }

        [AllowAnonymous]
        public ActionResult BrowseRotations()
        {
            return View("BrowseRotations");
        }

        [AllowAnonymous]
        public async Task<ActionResult> GetRotationsPage(int page = 1, int pageSize = 12)
        {
            var rotations = await _rotationService.GetPageAsync(page, pageSize);

            return Json(rotations, JsonRequestBehavior.AllowGet);
        }

        public async Task<IEnumerable<UserViewModel>> GetRotationMembers(Guid rotationId)
            => await _rotationService.GetRotationMembersAsync(rotationId);

        // TODO : Add validation, authorization
        public async Task JoinRotation(Guid userId, Guid rotationId)
            => await _rotationService.JoinRotationAsync(userId, rotationId);

        public async Task LeaveRotation(Guid userId, Guid rotationId)
            => await _rotationService.LeaveRotationAsync(userId, rotationId);

        public async Task DeleteRotation(Guid userId, Guid rotationId)
            => await _rotationService.DeleteRotation(userId, rotationId);
    }
}