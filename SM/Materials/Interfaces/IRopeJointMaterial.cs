using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public interface IRopeJointMaterial : IJointMaterial, IToBeFinalized
    {
        float MaxLength { get; }
        float2 AnchorA { get; }
        float2 AnchorB { get; }
    }
}
