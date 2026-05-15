using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cubic.Core.Entities
{
    public class User
    {
        public Guid Id { get; set; } = new Guid();
        
        [ForeignKey("Tenant")]
        public Guid TenantId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }=DateTime.Now;

        public Tenant Tenant { get; set; }
    }
}
