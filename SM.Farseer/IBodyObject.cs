using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Farseer
{
    public interface IBodyObject : IFarseerObject
    {
        void Set(float x, float y, float a);
        BodyType BodyType { get; }
        IEnumerable<FarseerPhysics.Dynamics.Fixture> GetAttachFixtures(Body body);
        IEnumerable<IPointObject> Points { get; }
    }

    public interface IBreakableBodyObject : IBodyObject
    {
        BodyManager Get(Body body, Vector2 originalPosition);
    }

    public interface IVector2Object : IFarseerObject
    {
        float X { get; }
        float Y { get; }
    }

    public interface IPointObject : IVector2Object
    {
        string ParentId { get; }
    }
}
