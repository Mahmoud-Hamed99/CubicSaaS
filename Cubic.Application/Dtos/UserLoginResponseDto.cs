using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cubic.Application.Dtos
{
    public class UserLoginResponseDto
    {
        public string Token { get; set; } = string.Empty;
        public Guid TenantId { get; set; }
        public string Role { get; set; } = string.Empty;
        public int ExpiresIn { get; set; }
    }
}
