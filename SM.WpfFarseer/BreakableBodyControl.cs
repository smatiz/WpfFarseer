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
    
    public class BreakableBodyPartControl : BodyControl
    {
        public override BodyType BodyType
        {
            get
            {
                return BodyType.Dynamic;
            }
        }
    }

    public class BreakableBodyControl : BodyControl
    {
        public override BodyType BodyType
        {
            get
            {
                return BodyType.Dynamic;
            }
         }
    }
}
