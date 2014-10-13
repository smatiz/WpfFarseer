using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfFarseer
{
    public class ShapeControl : BasicControl
    {
        public ShapeControl()
        {
            //Points = new PointCollection();
        }

        public PointCollection Points
        {
            get { return (PointCollection)GetValue(PointsProperty); }
            set { SetValue(PointsProperty, value); }
        }
        public static readonly DependencyProperty PointsProperty =
            DependencyProperty.Register("Points", typeof(PointCollection), typeof(ShapeControl), new PropertyMetadata(null));

        public virtual Shape Shape
        {
            get
            {
                var poly = new Polygon();
                _canvas.Children.Add(poly);
                foreach (var p in Points)
                {
                    poly.Points.Add(p);
                }
                return poly;
            }
        }
    }
}
