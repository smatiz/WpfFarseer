using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public interface IRopeJointMaterial : IJointMaterial
    {
        float MaxLength { get; set; }
        float2 AnchorA { get; set; }
        float2 AnchorB { get; set; }
        void Build(string id);

        string TargetNameA { get; set; }
        string TargetNameB { get; set; }
    }
}
