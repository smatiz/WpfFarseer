using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SM.Wpf
{
    public class TwoPointJointControlInfo
    {
        public BodyControl BodyControlA { get; private set; }
        public BodyControl BodyControlB { get; private set; }

        public Point AnchorA { get; private set; }
        public Point AnchorB { get; private set; }

        public TwoPointJointControlInfo(BodyControl bodyControlA, BodyControl bodyControlB, Point anchorA, Point anchorB)
        {
            BodyControlA = bodyControlA;
            BodyControlB = bodyControlB;
            AnchorA = anchorA;
            AnchorB = anchorB;
        }
    }
}
