using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Shapes;

namespace SM.Wpf
{
    public class TwoPointJointControlManager 
    {
        UIElement _parent;
        TwoPointJointControlInfo _ropeJointInfo;
        Line _line;

        public TwoPointJointControlManager(UIElement parent, TwoPointJointControlInfo ropeJointInfo, Line line)
        {
            _parent = parent;
            _ropeJointInfo = ropeJointInfo;
            _line = line;
        }

        public void Update()
        {
            //_line.X1 = _ropeJointInfo.BodyControlA._canvas.TranslatePoint(_ropeJointInfo.AnchorA, _parent).X;
            //_line.Y1 = _ropeJointInfo.BodyControlA._canvas.TranslatePoint(_ropeJointInfo.AnchorA, _parent).Y;
            //_line.X2 = _ropeJointInfo.BodyControlB._canvas.TranslatePoint(_ropeJointInfo.AnchorB, _parent).X;
            //_line.Y2 = _ropeJointInfo.BodyControlB._canvas.TranslatePoint(_ropeJointInfo.AnchorB, _parent).Y;
        }
    }
}
