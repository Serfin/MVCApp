using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using MVCApp.Core.Domain;
using MVCApp.Core.Enums;
using MVCApp.Infrastructure.Interfaces;
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

        public async Task<IEnumerable<Rotation>> GetPageAsync(int page, int pageSize)
        {
            var skip = (page - 1) * pageSize;
            var result = await _context.Rotations.OrderBy(x => x.Spots).Skip(skip).Take(pageSize).ToListAsync();

            return result;
        }

        public async Task<Rotation> GetByRotationId(Guid rotationId)
            => await _context.Rotations.SingleOrDefaultAsync(x => x.RotationId == rotationId);


        public async Task<IEnumerable<Rotation>> GetByUserId(Guid creator)
            => await _context.Rotations.Where(x => x.Creator == creator).ToListAsync();

        public async Task<IEnumerable<Rotation>> GetByTypeAsync(RotationType type)
            => await _context.Rotations.Where(x => x.Type == type.ToString()).ToListAsync();

        public async Task UpdateRotationAsync(Rotation rotation)
        {
            _context.Rotations.AddOrUpdate(rotation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRotationAsync(Guid rotationId)
        {
            var rotation = await GetByRotationId(rotationId);
            _context.Rotations.Remove(rotation);
            await _context.SaveChangesAsync();
        }
    }
}