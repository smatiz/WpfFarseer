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
            : base(parent)
        {
            _line = new Line();
            _line.Stroke = new SolidColorBrush(Colors.Green);
            _line.StrokeThickness = 1;
        }

        //float2 _anchorA;
        //float2 _anchorB;

        public float2 AnchorA { get; set; }
        //{
        //    get
        //    {
        //        return _anchorA;
        //    }
        //    set
        //    {
        //        _anchorA = value;
        //        _line.X1 = value.X * Context.Zoom;
        //        _line.Y1 = value.Y * Context.Zoom;
        //    }
        //}
        public float2 AnchorB { get; set; }
        //{
        //    get
        //    {
        //        return _anchorB;
        //    }
        //    set
        //    {
        //        _anchorB = value;
        //        _line.X2 = value.X * Context.Zoom;
        //        _line.Y2 = value.Y * Context.Zoom;
        //    }
        //}

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
