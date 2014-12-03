using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public partial interface IRopeJoint : IJoint
    {
        float MaxLength { get; }
        float MaxLengthFactor { get; }
        string TargetFlagIdA { get; }
        string TargetFlagIdB { get; }
    }
}
