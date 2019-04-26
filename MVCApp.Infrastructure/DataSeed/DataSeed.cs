using System;
using System.Threading.Tasks;
using AutoMapper;
using MVCApp.Core.Enums;
using MVCApp.Core.Repositories;
using MVCApp.Data.EntityFramework;
using MVCApp.Infrastructure.Repositories;
using MVCApp.Infrastructure.Services;

namespace MVCApp.Infrastructure.DataSeed
{
    public class DataSeed
    {
        private readonly IMapper _mapper;
        private readonly IEncrypter _encrypter;
        private readonly MVCAppContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IRotationRepository _rotationRepository;
        private readonly IAccountService _accountService;
        private readonly IRotationService _rotationService;

        public DataSeed(IMapper mapper, IEncrypter encrypter, MVCAppContext context, IUserRepository userRepository,
            IRotationRepository rotationRepository, IAccountService accountService, IRotationService rotationService)
        {
            _mapper = mapper;
            _encrypter = encrypter;
            _context = context;
            _userRepository = userRepository;
            _rotationRepository = rotationRepository;
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