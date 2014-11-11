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

namespace SM.Wpf
{
    public class RopeJointControl : BasicJointControl, IRopeJointView
    {
        public float MaxLength
        {
            get { return (float)GetValue(MaxLengthProperty); }
            set { SetValue(MaxLengthProperty, value); }
        }
        public static readonly DependencyProperty MaxLengthProperty =
            DependencyProperty.Register("MaxLength", typeof(float), typeof(RopeJointControl), new PropertyMetadata(-1f));

        public float MaxLengthFactor
        {
            get { return (float)GetValue(MaxLengthFactorProperty); }
            set { SetValue(MaxLengthFactorProperty, value); }
        }
        public static readonly DependencyProperty MaxLengthFactorProperty =
            DependencyProperty.Register("MaxLengthFactor", typeof(float), typeof(RopeJointControl), new PropertyMetadata(-1f));


        public string TargetFlagIdA
        {
            get { return (string)GetValue(TargetFlagIdAProperty); }
            set { SetValue(TargetFlagIdAProperty, value); }
        }
        public static readonly DependencyProperty TargetFlagIdAProperty =
            DependencyProperty.Register("TargetFlagIdA", typeof(string), typeof(RopeJointControl), new PropertyMetadata(null));

        public string TargetFlagIdB
        {
            get { return (string)GetValue(TargetFlagIdBProperty); }
            set { SetValue(TargetFlagIdBProperty, value); }
        }
        public static readonly DependencyProperty TargetFlagIdBProperty =
            DependencyProperty.Register("TargetFlagIdB", typeof(string), typeof(RopeJointControl), new PropertyMetadata(null));


        Line _line;
        public RopeJointControl()
        {
            _line = new Line();
            _line.Stroke = new SolidColorBrush(Colors.Green);
            _line.StrokeThickness = 1;
        }

        float2 _anchorA;
        float2 _anchorB;

        public float2 AnchorA
        {
            get
            {
                return _anchorA;
            }
            set
            {
                _anchorA = value;
                _line.X1 = value.X * _context.Zoom;
                _line.Y1 = value.Y * _context.Zoom;
            }
        }
        public float2 AnchorB
        {
            get
            {
                return _anchorB;
            }
            set
            {
                _anchorB = value;
                _line.X2 = value.X * _context.Zoom;
                _line.Y2 = value.Y * _context.Zoom;
            }
        }

        public void SetTargets(string targetBodyIdA, string targetBodyIdB)
        {
            TargetBodyIdA = targetBodyIdA;
            TargetBodyIdB = targetBodyIdB;
        }



        public string TargetBodyIdA { get; private set; }
        public string TargetBodyIdB { get; private set; }

        public UIElement UIElement
        {
            get
            {
                return _line;
            }
        }
    }
}
