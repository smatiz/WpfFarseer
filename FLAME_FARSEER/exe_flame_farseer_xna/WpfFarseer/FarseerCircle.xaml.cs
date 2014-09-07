using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework;
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
    /// <summary>
    /// Interaction logic for FarseerCircle.xaml
    /// </summary>
    public partial class FarseerCircle : UserControl
    {
        Body _body;

        public FarseerCircle()
        {
            InitializeComponent();

            switch (BodyType)
            {
                case BodyType.Static:
                    ellipse.Fill = new SolidColorBrush(Colors.Black);
                    break;
                case BodyType.Kinematic:
                    ellipse.Fill = new SolidColorBrush(Colors.Blue);
                    break;
                case BodyType.Dynamic:
                    ellipse.Fill = new SolidColorBrush(Colors.Orange);
                    break;
                default:
                    break;
            }

            /*Width = Radius * 2;
            Height = Width;
            Canvas.SetLeft(this, X - Radius);
            Canvas.SetTop(this, Y - Radius);*/
        }


        public double Radius { private get; set; }
        public double X { private get; set; }
        public double Y { private get; set; }
        public BodyType BodyType { private get; set; }

        void Update()
        {
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Width = Radius * 2;
            Height = Width;
            Canvas.SetLeft(this, X - Radius);
            Canvas.SetTop(this, Y - Radius);
            switch (BodyType)
            {
                case BodyType.Static:
                    ellipse.Fill = new SolidColorBrush(Colors.Black);
                    break;
                case BodyType.Kinematic:
                    ellipse.Fill = new SolidColorBrush(Colors.Blue);
                    break;
                case BodyType.Dynamic:
                    ellipse.Fill = new SolidColorBrush(Colors.Orange);
                    break;
                default:
                    break;
            }
            if (App.World != null)
            {
                _body = BodyFactory.CreateCircle(App.World, (float)Radius, Const.Density);


                _body.BodyType = BodyType;
                _body.Position = new Vector2((float)X, (float)Y);

                App.Tick += () =>
                {
                    Width = Radius * 2;
                    Height = Width;
                    Canvas.SetLeft(this, _body.Position.X - Radius);
                    Canvas.SetTop(this, _body.Position.Y - Radius);
                };
            }
        }
    }
}
