using System.Collections.Generic;
using System.Data.Entity;
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

        public async Task AddAsync(Rotation rotation)
        {
            _context.Rotations.Add(rotation);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Rotation>> GetAllAsync()
            => await _context.Rotations.ToListAsync();
    }
}