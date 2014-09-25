using FarseerPhysics.Dynamics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Farseer
{
    public interface IBodyObject : IFarseerObject
    {
        BodyType BodyType { get; }
        void Set(float x, float y, float a);
        IEnumerable<FarseerPhysics.Dynamics.Fixture> AttachFixtures(Body body);
        //IEnumerable<Fixture> Fixtures { get; }
    }
}
