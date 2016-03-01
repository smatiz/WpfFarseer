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
using SM.WpfView;
using SM.Farseer;
using SM.WpfFarseer;

namespace WpfFarseer
{
    /// <summary>
    /// Interaction logic for Sample5.xaml
    /// </summary>
    public partial class SamplePig : UserControl
    {
        public SamplePig()
        {
          //  SM.d.ping("uuuuuuu"); 
            InitializeComponent();
            Loaded += SamplePig_Loaded;
        }

        void SamplePig_Loaded(object sender, RoutedEventArgs e)
        {
           // SM.d.ping("uuuuuuu"); 
            //var img = new BitmapImage(new System.Uri(@"file://C:\Users\Developer\Desktop\TEMP\imm\aaa.bmp"));
            //uint[] us = img.GetData();
            ////for (int i = 0; i < n; i++)
            ////{
            ////    us[i] = (uint)bs[i * 4];
            ////}
            //var vs = FarseerPhysics.Common.TextureTools.TextureConverter.DetectVertices(us, (int)img.Width);
            //System.Windows.Shapes.Polygon poly = vs.ToWpfPolygon();
            //poly.Stroke = new SolidColorBrush(Colors.CadetBlue);

            //_resultCanvas.Children.Add(poly);
        }

        private void _farseerPlayer_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
