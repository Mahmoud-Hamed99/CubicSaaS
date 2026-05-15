using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cubic.Application.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; } = Guid.Empty;
        //public Guid? TenantId { get; set; }
        public string? FullName { get; set; }
        public string Email { get; set; }
        public string? Role { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }

    }
}
