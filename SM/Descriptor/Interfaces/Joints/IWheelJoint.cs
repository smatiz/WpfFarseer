using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public interface IWheelJoint : IJoint
    {
        float MotorSpeed { get;  }
        float MaxMotorTorque { get;}
        float Frequency { get;  }
        float DampingRatio { get; }
        float JointTranslatio { get; }
        float JointSpeed { get; }
        bool MotorEnabled { get; }
    }
}
