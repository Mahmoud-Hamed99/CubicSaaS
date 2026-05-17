using Cubic.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cubic.Core.Interfaces
{
    public interface ITenantRepository :IRepository<Tenant>
    {
        Task<bool> SlugExistsAsync(string slug);
        Task<Dictionary<Guid,int>> GetTenantActiveUsersCount();
    }
}
