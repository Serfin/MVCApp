using System;
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
            //await CreateUsers(100);
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
            Random rnd = new Random();

            for (int i = 0; i < rnd.Next(90,100); i++)
            {
                var guid = Guid.NewGuid();
                await _accountService.RegisterAsync(guid, $"test-email{i}", $"test-ign{i}",
                    "123qwe123", SystemRole.User);


                string test = $"test-ign{i}";
                int spots = rnd.Next(1, 6);
                LeagueName league;
                RotationType rotationType;

                switch (spots)
                {
                    case 1:
                        league = LeagueName.Beyond;
                        rotationType = RotationType.ChallengeRotation;
                        break;
                    case 2:
                        league = LeagueName.Breach;
                        rotationType = RotationType.CruelLabyrinth;
                        break;
                    case 3:
                        league = LeagueName.Delve;
                        rotationType = RotationType.MapRotation;
                        break;
                    default:
                        league = LeagueName.Beyond;
                        rotationType = RotationType.MasterRotation;
                        break;
                }

                await _rotationService.CreateRotationAsync(Guid.NewGuid(), guid, test, league, rotationType, spots);
            }
        }
    }
}