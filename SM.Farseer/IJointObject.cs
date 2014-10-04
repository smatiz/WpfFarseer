using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Farseer
{
    public interface IJointObject : IFarseerObject
    {
        bool CollideConnected { get; }
    }


    public interface ITwoPointJointObject : IJointObject
    {
        string TargetNameA { get; }
        string TargetNameB { get; }
    }

    public interface IRopeJointObject : ITwoPointJointObject
    {
        float MaxLength { get; }
        float MaxLengthFactor { get; }
    }

    public interface IWeldJointObject : ITwoPointJointObject
    {
        float ReferenceAngle { get; }
        float FrequencyHz { get; }
        float DampingRatio { get; }
    }

    public interface IRevoluteJointObject : ITwoPointJointObject
    { }
    public interface IDistanceJointObject : ITwoPointJointObject
    { }

}
