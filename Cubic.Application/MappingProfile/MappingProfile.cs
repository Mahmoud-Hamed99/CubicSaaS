using AutoMapper;
using Cubic.Application.Dtos;
using Cubic.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cubic.Application.MappingProfile
{
    public class MappingProfile : Profile
    {
        protected MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();

            CreateMap<Tenant, TenantDto>().ReverseMap();
        }
    }
}
