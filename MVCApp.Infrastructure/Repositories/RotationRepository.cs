using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ExileRota.Core.Domain;
using MVCApp.Core.Repositories;
using MVCApp.Data.EntityFramework;

namespace MVCApp.Infrastructure.Repositories
{
    public class RotationRepository : IRotationRepository
    {
        private readonly MVCAppContext _context;

        public RotationRepository(MVCAppContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rotation>> GetAllAsync()
            => await _context.Rotations.ToListAsync();
    }
}