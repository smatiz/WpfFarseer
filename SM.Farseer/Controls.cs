using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Farseer
{
    public interface IBodyControl : IIdentifiable
    {
        void Set(float x, float y, float a);
        BodyType BodyType { get; }
        IEnumerable<FarseerPhysics.Dynamics.Fixture> GetAttachFixtures(Body body);
        IEnumerable<IPointControl> FlagsPoints { get; }
    }

    public interface IBreakableBodyPartControl 
    {
        void Activate();
    }  
    
    //public interface IBreakableBodyControl : IBodyControl
    //{
    //    IEnumerable<IUpdatable> ChildrenBodyUpdater { get; }
    //}


    public interface IJointControl : IIdentifiable
    {
        bool CollideConnected { get; }
    }

    public interface ITwoPointJointControl : IJointControl
    {
        string TargetNameA { get; }
        string TargetNameB { get; }
    }

    public interface IRopeJointControl : ITwoPointJointControl
    {
        float MaxLength { get; }
        float MaxLengthFactor { get; }
    }

    public interface IWeldJointControl : ITwoPointJointControl
    {
        float ReferenceAngle { get; }
        float FrequencyHz { get; }
        float DampingRatio { get; }
    }

    public interface IRevoluteJointControl : ITwoPointJointControl
    { }
    public interface IDistanceJointControl : ITwoPointJointControl
    { }

}
