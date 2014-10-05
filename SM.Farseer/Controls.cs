using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Farseer
{
    public interface IBodyControl : IFarseerId
    {
        void Set(float x, float y, float a);
        BodyType BodyType { get; }
        IEnumerable<FarseerPhysics.Dynamics.Fixture> GetAttachFixtures(Body body);
        IEnumerable<IPointControl> Points { get; }
    }

    public interface IBreakableBodyControl : IBodyControl
    {
        IUpdatable Get(Body body, Vector2 originalPosition);
    }

    public interface IVector2Control : IFarseerId
    {
        float X { get; }
        float Y { get; }
    }

    public interface IPointControl : IVector2Control
    {
        string ParentId { get; }
    }

    public interface IJointControl : IFarseerId
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
