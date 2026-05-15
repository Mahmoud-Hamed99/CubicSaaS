using Cubic.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cubic.Application.Interfaces
{
    public interface ITenantService
    {
        Task<Result<bool>> CreateTenant(TenantDto dto);
        Task<Result<TenantDto>> GetTenantById(Guid id);
    }
}
