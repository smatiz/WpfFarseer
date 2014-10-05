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

namespace WpfFarseer
{
    public class BreakableBodyControl : BodyControl, IBreakableBodyControl
    {

        public BreakableBodyControl()
        {
            Loaded += (s, e) =>
            {
                foreach(var x in Children)
                {
                    //BodyFactory.CreateBreakableBody()
                }
            };
        }

        public IUpdatable Get(Body body, Vector2 originalPosition)
        {
            throw new NotImplementedException();
        }

    }
}
