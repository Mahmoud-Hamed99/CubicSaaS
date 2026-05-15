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
        private readonly IUserRepository _userRepo;
        private readonly IUnitOfWork _uow;
        private readonly ILogger<UserJobs> _logger;

        public UserJobs(IUserRepository userRepo, IUnitOfWork unitOfWork, ILogger<UserJobs> logger)
        {
            _userRepo = userRepo;
            _logger = logger;
            _uow = unitOfWork;
        }

        public async Task LogActiveUsersPerTenantAsync()
        {
            var tenants = await _uow.GetRepository<Tenant>().GetAllAsync();

            foreach (var tenant in tenants)
            {
               int count=  _userRepo.GetUsersCountByTenantId(tenant.Id);

                _logger.LogInformation(
                    "Tenant {TenantId} has {Count} active users",
                    tenant.Id,
                    count);
            }
        }

    }
}
