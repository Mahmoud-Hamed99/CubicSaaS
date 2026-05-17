using Cubic.Application.Dtos;
using Cubic.Application.Interfaces;
using Cubic.Core.Interfaces;
using System.Net;

namespace Cubic.Application.Implmentations
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _tokenGenerator;  

        public AuthService( IJwtTokenGenerator tokenGenerator, IUserRepository userRepository)
        {
            _tokenGenerator = tokenGenerator;
            _userRepository = userRepository;
        }

        public async Task<Result<UserLoginResponseDto>> Login(UserLoginDto dto)
        {
           
            var user = await _userRepository.GetUserById(dto.UserId);
           
            if (user is null)
               return Result<UserLoginResponseDto>.Failed("User not found", HttpStatusCode.NotFound);

            var token = _tokenGenerator.GenerateToken(
                user.Id,
                user.TenantId,
                user.Email,
                user.Role
            );

            return Result<UserLoginResponseDto>.Success(new UserLoginResponseDto
            {
                Token = token,
                TenantId = user.TenantId,
                Role = user.Role,
                ExpiresIn = 3600 
            });
        }
    }
}
