using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfFarseer
{
    public class RopeJointInfo
    {
        public BodyControl BodyControlA { get; private set; }
        public BodyControl BodyControlB { get; private set; }

        public Point AnchorA { get; private set; }
        public Point AnchorB { get; private set; }

        public RopeJointInfo(BodyControl bodyControlA, BodyControl bodyControlB, Point anchorA, Point anchorB)
        {
            BodyControlA = bodyControlA;
            BodyControlB = bodyControlB;
            AnchorA = anchorA;
            AnchorB = anchorB;
        }
    }
}
