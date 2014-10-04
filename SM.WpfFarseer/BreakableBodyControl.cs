using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using SM.Farseer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfFarseer;

namespace SM.WpfFarseer
{
    class BreakableBodyControl : BasicControl //, IBreakableBodyObject
    {

        //public BodyUpdater Get(Body body, Vector2 originalPosition)
        //{
        //    return new BodyUpdater(new BodyControl(), body, originalPosition);
        //}

        //public BodyManager Get(FarseerPhysics.Dynamics.Fixture fixture)
        //{
        //     var body = BodyFactory.CreateBody(_world, Vector2.Zero);
        //    return new BodyManager(new BodyControl(), )
        //}

        public void Set(float x, float y, float a)
        {

        }

        public FarseerPhysics.Dynamics.BodyType BodyType
        {
            get { return FarseerPhysics.Dynamics.BodyType.Dynamic; }
        }

        public IEnumerable<FarseerPhysics.Dynamics.Fixture> GetAttachFixtures(FarseerPhysics.Dynamics.Body body)
        {
            throw new NotImplementedException();
        }

        

      
    }
}
