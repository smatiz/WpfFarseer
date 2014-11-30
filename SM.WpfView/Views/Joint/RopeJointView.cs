using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using SM;
using System.Windows.Media;

namespace SM.WpfView
{
    public class RopeJointView : BasicJointView, IRopeJointView
    {
        Line _line;

        public RopeJointView(BasicView parent, IRopeJoint joint)
            : base(parent, joint.Id)
        {
            _line = new Line();
            _line.Stroke = new SolidColorBrush(Colors.Green);
            _line.StrokeThickness = 1;
        }

        public float2 AnchorA { get; set; }
        public float2 AnchorB { get; set; }

        public override void Update()
        {
            _line.X1 = AnchorA.X * Context.Zoom;
            _line.Y1 = AnchorA.Y * Context.Zoom;

            _line.X2 = AnchorB.X * Context.Zoom;
            _line.Y2 = AnchorB.Y * Context.Zoom;
        }

        public UIElement UIElement
        {
            get
            {
                return _line;
            }
        }
    }
}
