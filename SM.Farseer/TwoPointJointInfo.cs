using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SM.Farseer
{
    public class TwoPointJointInfo
    {
        public Body BodyControlA { get; private set; }
        public Body BodyControlB { get; private set; }

        public Vector2 AnchorA { get; private set; }
        public Vector2 AnchorB { get; private set; }

        public TwoPointJointInfo(Body bodyControlA, Body bodyControlB, Vector2 anchorA, Vector2 anchorB)
        {
            BodyControlA = bodyControlA;
            BodyControlB = bodyControlB;
            AnchorA = anchorA;
            AnchorB = anchorB;
        }
    }
}
