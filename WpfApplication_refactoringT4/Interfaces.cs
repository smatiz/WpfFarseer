using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication_refactoringT4
{
    interface IJoint
    {
    }
    interface IJoint1 : IJoint
    {
    }
    interface IJoint2 : IJoint
    {
    }

    interface IWheelJoint : IJoint
    {
        float MotorSpeed { get; set; }
        float MaxMotorTorque { get; set; }
        float Frequency { get; set; }
        float DampingRatio { get; set; }
        float JointTranslatio { get; set; }
        float JointSpeed { get; set; }
        bool MotorEnabled { get; set; }
    }
}
