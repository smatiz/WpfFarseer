using SM;
using SM.Wpf;
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

namespace SM.Wpf
{


    public class ShapeControl : DependencyObject, IShape
    {
        public ShapeControl()
        {
            Points = new PointCollection();
        }

        public PointCollection Points
        {
            get { return (PointCollection)GetValue(PointsProperty); }
            set { SetValue(PointsProperty, value); }
        }
        public static readonly DependencyProperty PointsProperty =
            DependencyProperty.Register("Points", typeof(PointCollection), typeof(ShapeControl), new PropertyMetadata(null, new PropertyChangedCallback(Points_PropertyChanged)));
        static void Points_PropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }
        public virtual UIElement Shape
        {
            get
            {
                var poly = new Polygon();
                foreach (var p in Points)
                {
                    poly.Points.Add(p);
                }
                return poly;
            }
        }


        public IEnumerable<float2> Points_X
        { 
            get
            {
                foreach(var p in Points)
                {
                    yield return new SM.float2((float)p.X, (float)p.Y);
                }
            }
         }



        public float Density
        {
            get { return (float)GetValue(DensityProperty); }
            set { SetValue(DensityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Density.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DensityProperty =
            DependencyProperty.Register("Density", typeof(float), typeof(ShapeControl), new PropertyMetadata(1f));

        

        
        public float Density_X
        {
            get
            {
                return Density; 
            }
        }

    }
}
