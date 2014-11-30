using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public interface IRopeJoint : IJoint
    {
        float MaxLength { get; }
        float MaxLengthFactor { get; }
        string TargetFlagIdA { get; }
        string TargetFlagIdB { get; }
        bool CollideConnected { get; }
    }
}
