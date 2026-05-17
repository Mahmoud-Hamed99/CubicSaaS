using AutoMapper;
using Cubic.Application.Dtos;
using Cubic.Application.Interfaces;
using Cubic.Core.Entities;
using Cubic.Core.Interfaces;
using FluentValidation;

namespace Cubic.Application.Implmentations
{
    public class UserService : IUserService
    {
        private readonly IValidator<UserDto> _validator;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITenantContext _tenantContext;

        public UserService(IUserRepository userRepository, IValidator<UserDto> validator, IUnitOfWork unitOfWork, IMapper mapper, ITenantContext tenantContext)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _tenantContext = tenantContext;
            _validator = validator;
        }

        public async Task<Result<bool>> CreateUser(UserDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return Result<bool>.Failed(string.Join(", ", errors) , System.Net.HttpStatusCode.BadRequest);
            }

            var isEmailExist = _userRepository.EmailExistsAsync(dto.Email, _tenantContext.TenantId.GetValueOrDefault());
            if (isEmailExist) 
                return Result<bool>.Failed("Email already exists");

            var entity = _mapper.Map<User>(dto);
            
            entity.TenantId = _tenantContext.TenantId.GetValueOrDefault();

            await _unitOfWork.GetRepository<User>().AddAsync(entity);
         
            return  await _unitOfWork.SaveChangesAsync() > 0 ? 
                Result<bool>.Success(true,"Created Successfully..!") 
              : Result<bool>.Failed("Failed to create user");

        }

        public async Task<Result<bool>> DeleteUser(Guid id)
        {
          return _userRepository.MarkUserAsDeleted(id ,_tenantContext.TenantId.GetValueOrDefault()) ?
                   Result<bool>.Success(true, "Deleted Successfully..!")
              : Result<bool>.Failed("Failed to delete user");
        }

        public async Task<Result<IEnumerable<UserDto>>> GetAllUsers()
        {
          var entities= await _unitOfWork.GetRepository<User>().GetAllAsync();
           
          var userDtos = _mapper.Map<IEnumerable<UserDto>>(entities);

          return Result<IEnumerable<UserDto>>.Success(userDtos, "Users retrieved successfully");
        }

        public async Task<Result<bool>> UpdateUser(Guid id, UserDto dto)
        {
            var validationResult = await _validator.ValidateAsync(dto);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                     .Select(e => e.ErrorMessage)
                     .ToList();

                return Result<bool>.Failed(string.Join(", ", errors), System.Net.HttpStatusCode.BadRequest);
            }

            var user= await _userRepository.GetByIdAsync(id);
            
            if (user == null || user.TenantId != _tenantContext.TenantId)
                return Result<bool>.Failed("User not found.");

            user.FullName = dto.FullName;
            user.Email = dto.Email;
            user.Role = dto.Role;
            user.IsActive = dto.IsActive;   

            _unitOfWork.GetRepository<User>().Update(user);
          
            return await _unitOfWork.SaveChangesAsync() > 0 ?
                Result<bool>.Success(true, "Updated Successfully..!") :
                Result<bool>.Failed("Failed to update user");

        }

       
    }
}
