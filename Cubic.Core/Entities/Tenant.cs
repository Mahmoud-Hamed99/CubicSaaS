using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cubic.Core.Entities
{
    public class Tenant
    {
       public Guid Id { get; set; } = new Guid();
        public string Name { get; set; }
        public string Slug { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<User> Users { get; set; }
    }
}
