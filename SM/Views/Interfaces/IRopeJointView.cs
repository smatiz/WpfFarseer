using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public interface IRopeJointView : IJointView
    {
        float2 AnchorA { set; }
        float2 AnchorB { set; }
    }
}
