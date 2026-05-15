using Cubic.Core.Entities;
using Cubic.Core.Interfaces;
using Cubic.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Cubic.Infrastructure.Implmentations
{
    public class TenantRepository : Repository<Tenant>,ITenantRepository
    {
        private readonly AppDbContext _context;

        public TenantRepository(AppDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<bool> SlugExistsAsync(string slug)
        {
           return await _context.Tenant.AnyAsync(t => t.Slug.Trim().ToLower() == slug.Trim().ToLower());
        }
    }
}
