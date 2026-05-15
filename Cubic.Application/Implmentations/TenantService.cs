using AutoMapper;
using Cubic.Application.Dtos;
using Cubic.Application.Interfaces;
using Cubic.Core.Entities;
using Cubic.Core.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cubic.Application.Implmentations
{
    public class TenantService : ITenantService
    {
        private readonly IValidator<TenantDto> _validator;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantRepository _tenantRepository;
        private readonly IMapper _mapper;
        public TenantService(IUnitOfWork unitOfWork, ITenantRepository tenantRepository, IMapper mapper, IValidator<TenantDto> validator)
        {
            _unitOfWork = unitOfWork;
            _tenantRepository = tenantRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<Result<bool>> CreateTenant(TenantDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return Result<bool>.Failed(string.Join(", ", errors), System.Net.HttpStatusCode.BadRequest);
            }
            var isExist = await _tenantRepository.SlugExistsAsync(dto.Slug);

            if (isExist)
                return Result<bool>.Failed("Slug already exists.");
           
           var entity = _mapper.Map<Tenant>(dto);
             
           await _unitOfWork.GetRepository<Tenant>().AddAsync(entity);
           
            var result = await _unitOfWork.SaveChangesAsync() > 0;

            return result ? Result<bool>.Success(true, "Tenant created successfully.") 
                : Result<bool>.Failed("Failed to create tenant.");
        }

        public async Task<Result<TenantDto>> GetTenantById(Guid id)
        {
            var tenant = await _tenantRepository.GetByIdAsync(id);
           
            if (tenant == null)
                return Result<TenantDto>.Failed("Tenant not found.");

            var dto = _mapper.Map<TenantDto>(tenant);
        
            return Result<TenantDto>.Success(dto);
        }
    }
}
