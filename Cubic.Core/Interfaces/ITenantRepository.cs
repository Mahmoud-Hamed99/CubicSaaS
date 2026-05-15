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
        // add your custom methods for tenant repository here
        Task<bool> SlugExistsAsync(string slug);
    }
}
