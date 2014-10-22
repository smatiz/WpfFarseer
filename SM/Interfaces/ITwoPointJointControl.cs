using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public interface ITwoPointJointControl : IJointControl
    {
        string TargetFlagIdA { get; }
        string TargetFlagIdB { get; }
    }
}
