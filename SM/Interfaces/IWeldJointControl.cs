using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public interface IWeldJointControl : ITwoPointJointControl
    {
        float ReferenceAngle { get; }
        float FrequencyHz { get; }
        float DampingRatio { get; }
    }
}
