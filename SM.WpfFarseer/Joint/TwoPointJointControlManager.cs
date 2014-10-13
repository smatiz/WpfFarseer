using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Shapes;

namespace WpfFarseer
{
    public class TwoPointJointControlManager 
    {
        FarseerCanvas _farseerCanvas;
        TwoPointJointControlInfo _ropeJointInfo;
        Line _line;

        public TwoPointJointControlManager(FarseerCanvas farseerCanvas, TwoPointJointControlInfo ropeJointInfo, Line line)
        {
            _farseerCanvas = farseerCanvas;
            _ropeJointInfo = ropeJointInfo;
            _line = line;
        }

        public void Update()
        {
            _line.X1 = _ropeJointInfo.BodyControlA._canvas.TranslatePoint(_ropeJointInfo.AnchorA, _farseerCanvas).X;
            _line.Y1 = _ropeJointInfo.BodyControlA._canvas.TranslatePoint(_ropeJointInfo.AnchorA, _farseerCanvas).Y;
            _line.X2 = _ropeJointInfo.BodyControlB._canvas.TranslatePoint(_ropeJointInfo.AnchorB, _farseerCanvas).X;
            _line.Y2 = _ropeJointInfo.BodyControlB._canvas.TranslatePoint(_ropeJointInfo.AnchorB, _farseerCanvas).Y;
        }
    }
}
