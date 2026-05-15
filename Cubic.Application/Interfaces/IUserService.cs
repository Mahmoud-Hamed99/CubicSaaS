using Cubic.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cubic.Application.Interfaces
{
    public interface IUserService
    {
        Task<Result<IEnumerable<UserDto>>> GetAllUsers();
        Task<Result<bool>> CreateUser(UserDto dto);
        Task<Result<bool>> UpdateUser(Guid id, UserDto dto);
        Task<Result<bool>> DeleteUser(Guid id);
    }
}
