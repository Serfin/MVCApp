﻿using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using MVCApp.Core.Enums;
using MVCApp.Infrastructure.Interfaces;

namespace MVCApp.Presentation.Controllers
{
    public class AccountController : Controller
    {
        public async Task<ActionResult> Index()
        {
            await CreateUsers(100);
            return View();
        }

        private readonly IUserService _accountService;
        private readonly IRotationService _rotationService;

        public AccountController(IUserService accountService, IRotationService rotationService)
        {
            _accountService = accountService;
            _rotationService = rotationService;
        }

        public async Task CreateUsers(int amount)
        {
            for (int i = 0; i < 100; i++)
            {
                var guid = Guid.NewGuid();
                await _accountService.RegisterAsync(guid, $"test-email{i}", $"test-ign{i}",
                    "123qwe123", SystemRole.User);

                Random rnd = new Random();

                await _rotationService.CreateRotationAsync(Guid.NewGuid(), guid, LeagueName.Delve, RotationType.CruelLabyrinth, rnd.Next(1, 5));
            }
        }
    }
}