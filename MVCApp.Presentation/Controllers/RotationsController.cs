using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using MVCApp.Common.ViewModels;
using MVCApp.Infrastructure.Interfaces;

namespace MVCApp.Presentation.Controllers
{
    public class RotationsController : Controller
    {
        private readonly IRotationService _rotationService;

        public RotationsController(IRotationService rotationService)
        {
            _rotationService = rotationService;
        }

        public ActionResult BrowseRotations()
        {
            return View("BrowseRotations");
        }
        public async Task<ActionResult> GetRotationsPage(int page = 1, int pageSize = 12)
        {
            var rotations = await _rotationService.GetPageAsync(page, pageSize);

            return Json(rotations, JsonRequestBehavior.AllowGet);
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