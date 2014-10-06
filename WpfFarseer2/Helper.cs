using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using W = System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;


namespace WpfFarseer
{
    public class Helper
    {
        FarseerPhysics.Dynamics.World w = new FarseerPhysics.Dynamics.World(new Vector2(0, 10));
        W.Point P(Canvas c)
        {
            return new W.Point(Canvas.GetLeft(c), Canvas.GetTop(c));
        }

        Vector2 V(Canvas c)
        {
            return new Vector2((float)Canvas.GetLeft(c), (float)Canvas.GetTop(c));
        }

        public static Polygon Poly(FarseerPhysics.Common.Vertices vs, System.Windows.Media.Color c)
        {
            var poly = new Polygon();
            foreach (var v in vs)
            {
                poly.Points.Add(new W.Point(v.X, v.Y));
            }
            poly.Fill = new SolidColorBrush(c);
            poly.Stroke = new SolidColorBrush(Colors.Black);
            poly.StrokeThickness = 1;
            return poly;
        }

        FarseerPhysics.Common.Vertices VS(Canvas c)
        {
            FarseerPhysics.Common.Vertices vs = new FarseerPhysics.Common.Vertices();
            foreach (var x in c.Children)
            {
                vs.Add(V(x as Canvas));
            }
            return vs;
        }

    }
}
