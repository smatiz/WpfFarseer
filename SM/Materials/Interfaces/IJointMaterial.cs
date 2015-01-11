using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public interface IJointMaterial : IMaterial, IToBeFinalized
    {
        float Breakpoint { get; set; }
        bool CollideConnected { get; set; }
    }
}
