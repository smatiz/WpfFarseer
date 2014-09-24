using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;

namespace WpfFarseer
{
    public class TwoPointJointManager 
    {
        FarseerCanvas _farseerCanvas;
        TwoPointJointInfo _ropeJointInfo;
        Line _line;

        public TwoPointJointManager(FarseerCanvas farseerCanvas, TwoPointJointInfo ropeJointInfo, Line line)
        {
            _farseerCanvas = farseerCanvas;
            _ropeJointInfo = ropeJointInfo;
            _line = line;
        }

        public void Update()
        {
            _line.X1 = _ropeJointInfo.BodyControlA.TranslatePoint(_ropeJointInfo.AnchorA, _farseerCanvas).X;
            _line.Y1 = _ropeJointInfo.BodyControlA.TranslatePoint(_ropeJointInfo.AnchorA, _farseerCanvas).Y;
            _line.X2 = _ropeJointInfo.BodyControlB.TranslatePoint(_ropeJointInfo.AnchorB, _farseerCanvas).X;
            _line.Y2 = _ropeJointInfo.BodyControlB.TranslatePoint(_ropeJointInfo.AnchorB, _farseerCanvas).Y;
        }
    }
}
