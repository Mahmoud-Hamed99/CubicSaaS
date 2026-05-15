using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cubic.Core.Interfaces
{
    public interface ITenantContext
    {
        Guid? TenantId { get; }
    }
}
