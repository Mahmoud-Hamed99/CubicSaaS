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

        public bool EmailExistsAsync(string email, Guid tenantId)
        {
            return  _context.Users.Any(u => u.Email.Trim().ToLower() == email.Trim().ToLower() && u.TenantId == tenantId);
        }

        public int GetUsersCountByTenantId(Guid tenantId)
        {
           return _context.Users.Count(u => u.TenantId == tenantId && u.IsActive);
        }

        public bool MarkUserAsDeleted(Guid userId, Guid tenantId)
        {
            var user=  _context.Users.FirstOrDefault(u => u.Id == userId && u.TenantId == tenantId);
            
            if (user == null)
                return false;

            user.IsActive = false;
            
            _context.SaveChanges();
            
            return true;

        }
    }
}
