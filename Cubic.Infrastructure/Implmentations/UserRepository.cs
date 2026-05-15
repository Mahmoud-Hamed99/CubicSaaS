using Cubic.Core.Entities;
using Cubic.Core.Interfaces;
using Cubic.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cubic.Infrastructure.Implmentations
{
    public class UserRepository :Repository<User>,IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<bool> EmailExistsAsync(string email, Guid tenantId)
        {
            return await _context.Users.AnyAsync(u => u.Email.Trim().ToLower() == email.Trim().ToLower() && u.TenantId == tenantId);
        }
    }
}
