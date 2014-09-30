using SM.Farseer;
using FarseerPhysics.Common;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Media;

namespace WpfFarseer
{
    public static class WpfFarseerHelper
    {
        public static Vertices ToFarseer(IEnumerable<System.Windows.Point> points)
        {
            return new Vertices(from p in points select p.ToFarseer());
        }
        public static Vertices ToFarseer(this UIElement uielement, Polygon poly)
        {
            return ToFarseer(from p in poly.Points select poly.TranslatePoint(p, uielement));
        }
        public static Fixture ToFarseer(this UIElement uielement, Shape shape, Body body)
        {
            if (shape is Polygon)
            {
                return FixtureFactory.AttachPolygon(uielement.ToFarseer((Polygon)shape), BodyControl.GetDensity(shape), body);
            }
            else if (shape is System.Windows.Shapes.Path)
            {

                return FixtureFactory.AttachPolygon(uielement.ToFarseer((Polygon)shape), BodyControl.GetDensity(shape), body);
            }
            else
            {
                return FixtureFactory.AttachCircle(1, 1, body);
            }
        }

        

        public static Vector2 ToFarseer(this System.Windows.Point p)
        {
            return new Vector2((float)p.X, (float)p.Y);
        }

        public static System.Windows.Point ToWpf(this Vector2 p)
        {
            return new System.Windows.Point(p.X, p.Y);
        }

        public static Body ToFarseer(this BodyControl b, FarseerWorldManager f)
        {
            return (Body)f.Find(b.Id);
        }

        public static TwoPointJointInfo ToFarseer(this TwoPointJointControlInfo x, FarseerWorldManager f)
        {
            return new TwoPointJointInfo(x.BodyControlA.ToFarseer(f), x.BodyControlB.ToFarseer(f), x.AnchorA.ToFarseer(), x.AnchorB.ToFarseer());
        }


        //public static Shape ToFarseer(this Path x)
        //{

        //    // xaml to png
        //    // png to farseer


        //    //Texture2D alphabet = ScreenManager.Content.Load<Texture2D>("Samples/alphabet");

        //    //uint[] data = new uint[alphabet.Width * alphabet.Height];
        //    //alphabet.GetData(data);

        //    //List<Vertices> list = PolygonTools.CreatePolygon(data, alphabet.Width, 3.5f, 20, true, true);







        //   // ((PathFigure)((System.Windows.Media.PathGeometry)x.Data).Figures[0]).



        //}
    }
}
