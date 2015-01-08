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

        public RopeJointView(Canvas parentCanvas, IContext context, IRopeJoint joint, Views views)
            : base(parentCanvas, context, joint.Id)
        {
            _line = new Line();
            _line.Stroke = new SolidColorBrush(Colors.Blue);
            _line.StrokeThickness = 1;
            var canvas = new Canvas();
            canvas.Children.Add(_line);
            AddChild(canvas);
            AnchorA = views.FindFlag(joint.TargetFlagIdA).P;
            AnchorB = views.FindFlag(joint.TargetFlagIdB).P;
            Update();
        }

        public float2 AnchorA { private get; set; }
        public float2 AnchorB { private get; set; }

        public override void Update()
        {
            _line.X1 = AnchorA.X * Context.Zoom;
            _line.Y1 = AnchorA.Y * Context.Zoom;

            _line.X2 = AnchorB.X * Context.Zoom;
            _line.Y2 = AnchorB.Y * Context.Zoom;
        }
    }
}
