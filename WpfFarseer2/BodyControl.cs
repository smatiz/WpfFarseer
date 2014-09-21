using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfFarseer
{
   // incapsula il body
    // crea il Body e le fixture in base allo xaml
    // sposta il canvas in base alla fisica del body

    public class BodyControl : Canvas
    {
        BodyController _body;
        RotateTransform _rotation;
       TranslateTransform _traslation;


        public FarseerPhysics.Dynamics.BodyType BodyType
        {
            get { return (FarseerPhysics.Dynamics.BodyType)GetValue(BodyTypeProperty); }
            set { SetValue(BodyTypeProperty, value); }
        }
        public static readonly DependencyProperty BodyTypeProperty =
            DependencyProperty.Register("BodyType", typeof(FarseerPhysics.Dynamics.BodyType), typeof(BodyControl), new PropertyMetadata(FarseerPhysics.Dynamics.BodyType.Static));

        public BodyControl()
        {
            _rotation = new RotateTransform();
            _traslation = new TranslateTransform();

            Loaded += (s, e) =>
            {
                var rt = this.RenderTransform;
                var gt = new TransformGroup();
                gt.Children.Add(_rotation);
                gt.Children.Add(_traslation);
                gt.Children.Add(rt);
                this.RenderTransform = gt;
                refreshVisual();
            };
        }

        //public void SetBody(Body body)
        //{
        //    _body = body;

        //    //WpfDebugView.Instance.Add(_body);
        //}
        private static string uniqueCode()
        {
            string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";
            string ticks = DateTime.UtcNow.Ticks.ToString();
            var code = "";
            for (var i = 0; i < characters.Length; i += 2)
            {
                if ((i + 2) <= ticks.Length)
                {
                    var number = int.Parse(ticks.Substring(i, 2));
                    if (number > characters.Length - 1)
                    {
                        var one = double.Parse(number.ToString().Substring(0, 1));
                        var two = double.Parse(number.ToString().Substring(1, 1));
                        code += characters[Convert.ToInt32(one)];
                        code += characters[Convert.ToInt32(two)];
                    }
                    else
                        code += characters[number];
                }
            }
            return code;
        }

        public void Initialize(WorldManager worldManager)
        {
            if (Name == "")
            {
                // funziona in una sessione ma non funziona al restart dell'applicazione
                Name = uniqueCode();
            }

            var p = WpfFarseerHelper.ToFarseer(TranslatePoint(new System.Windows.Point(0, 0), (UIElement)Parent));
            _body = worldManager.CreateBody(p, BodyType, Name, this, FindShapes());

            //WpfDebugView.Instance.Add(_body);


           
        }

        public void Update(WorldManager worldManager)
        {
            refreshVisual();
            if (_body == null) return;
            _body.Update(_traslation, _rotation);
            
        }

        private Canvas CanvasParent
        {
            get
            {
                return Parent as Canvas;
            }
        }
        private Brush GetBrush()
        {
            switch (BodyType)
            {
                case FarseerPhysics.Dynamics.BodyType.Static:
                    return new SolidColorBrush(Colors.Black);
                case FarseerPhysics.Dynamics.BodyType.Kinematic:
                    return new SolidColorBrush(Colors.Blue);
                case FarseerPhysics.Dynamics.BodyType.Dynamic:
                    return new SolidColorBrush(Colors.Orange);
                default:
                    return null;
            }
        }
        private void refreshVisual()
        {
            var brush =  GetBrush();

            foreach (var shape in FindShapes())
            {
                shape.Fill = brush;
            }

        }
        private IEnumerable<Shape> FindShapes()
        {
            foreach (var x in Children)
            {
                if (x is Shape)
                {
                    yield return x as Shape;
                }
            }
        }



        public static float GetDensity(DependencyObject obj)
        {
            return (float)obj.GetValue(DensityProperty);
        }

        public static void SetDensity(DependencyObject obj, float value)
        {
            obj.SetValue(DensityProperty, value);
        }

        // Using a DependencyProperty as the backing store for Density.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DensityProperty =
            DependencyProperty.RegisterAttached("Density", typeof(float), typeof(BodyControl), new PropertyMetadata(Const.Density));

        
        
    }
}
