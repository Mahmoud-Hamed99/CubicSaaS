using Cubic.Core.Entities;
using Cubic.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cubic.Application.Jobs
{
    public class UserJobs
    {
      
        private readonly ILogger<UserJobs> _logger;
        private readonly ITenantRepository _tenantRepo;
        public UserJobs(ILogger<UserJobs> logger, ITenantRepository tenantRepo)
        {
            _logger = logger;
            _tenantRepo = tenantRepo;
        }

        public async Task LogActiveUsersPerTenantAsync()
        {
            var activeUsersCount = await _tenantRepo.GetTenantActiveUsersCount();

            foreach (var kvp in activeUsersCount)
            {
                _logger.LogInformation(
                    "Tenant {TenantId} has {Count} active users",
                    kvp.Key,
                    kvp.Value);
            }
        }

    }
}



         