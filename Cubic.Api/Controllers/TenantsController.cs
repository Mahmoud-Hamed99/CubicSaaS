using Cubic.Application.Dtos;
using Cubic.Application.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cubic.Api.Controllers
{
    [Authorize(Roles = "admin")]
    [ApiController]
    [Route("api/tenants")]
    public class TenantsController : ControllerBase
    {
        private readonly ITenantService _tenantService;
     
        public TenantsController(ITenantService tenantService)
        {
                _tenantService = tenantService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTenant([FromBody] TenantDto dto)
        {
            var res = await _tenantService.CreateTenant(dto);
            return Ok(res);
        }


        [HttpGet]
        public async Task<IActionResult> GetTenantById(Guid Id)
        {
            var res = await _tenantService.GetTenantById(Id);
            
            return Ok(res);
        }
    }
}
