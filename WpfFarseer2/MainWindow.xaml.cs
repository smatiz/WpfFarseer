using SM;
using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class MainWindow : Window
    {
        public uint[] xxx(Microsoft.Xna.Framework.Graphics.Texture2D img)
        {
            var w = img.Width;
            var h = img.Height;
            int n = w * h;
            var array = new uint[n];
            img.GetData<uint>(array);
            return array;
        }

        public uint[] xxx(System.Drawing.Bitmap img)
        {
            var w = img.Width;
            var h = img.Height;
            int n = w * h;
            var array = new uint[n];
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    System.Drawing.Color pixel = img.GetPixel(i, j);
                    if (pixel.ToArgb() == System.Drawing.Color.White.ToArgb())
                    {
                        array[i * (h - 1) + j] = (uint)System.Drawing.Color.FromArgb(0, 0, 0, 0).ToArgb();
                    }
                    else //if (pixel == System.Drawing.Color.Black)
                    {
                        array[i * (h - 1) + j] = (uint)System.Drawing.Color.FromArgb(255, 255, 255, 255).ToArgb();
                    }
                }
            }

            return array;
        }

        public uint[] xxx2(System.Drawing.Bitmap img)
        {
            var w = img.Width;
            var h = img.Height;
            int n = w * h * 3;
            var array = new uint[n];
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    System.Drawing.Color pixel = img.GetPixel(i, j);
                    array[(i * (h - 1) + j) * 3] = pixel.R;
                    array[(i * (h - 1) + j) * 3 + 1] = pixel.G;
                    array[(i * (h - 1) + j) * 3 + 2] = pixel.B;
                }
            }

            return array;
        }

        public uint[] xxx(BitmapImage img)
        {
            var w = (int)img.Width;
            var h = (int)img.Height;
            int n = (int)(w * h);
            var array = new uint[n];
            for (int i = 0; i < img.Width; i++)
            {
                for (int j = 0; j < img.Height; j++)
                {
                    //var pixel = img.pi(i, j);
                    //array[i * (h - 1) + j] = pixel.R;
                }
            }

            return array;
        }



        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var img = new System.Drawing.Bitmap(@"C:\Users\Developer\Desktop\aaa.bmp");
            
            
            
            //var gd = new Microsoft.Xna.Framework.Graphics.GraphicsDevice(Microsoft.Xna.Framework.Graphics.GraphicsAdapter.DefaultAdapter, Microsoft.Xna.Framework.Graphics.GraphicsProfile.Reach, new Microsoft.Xna.Framework.Graphics.PresentationParameters());
            //Microsoft.Xna.Framework.Graphics.Texture2D fileTexture;
            //using (FileStream fileStream = new FileStream(@"C:\Images\Box.png", FileMode.Open))
            //{
            //    fileTexture = Microsoft.Xna.Framework.Graphics.Texture2D.FromStream(gd, fileStream);
            //}
            ////var img2 = Microsoft.Xna.Framework.Graphics.Texture2D.FromStream(gd, @"C:\Users\Developer\Desktop\aaa.bmp");
            //uint[] us = xxx(fileTexture);

            uint[] us = xxx2(img);


            //for (int i = 0; i < n; i++)
            //{
            //    us[i] = (uint)bs[i * 4];
            //}
            var vs = FarseerPhysics.Common.TextureTools.TextureConverter.DetectVertices(us, (int)img.Width);
            _resultCanvas.Children.Add(Helper.Poly(vs, Colors.CadetBlue));
            //BitmapImage image = new BitmapImage(new Uri(@"C:\Users\Developer\Desktop\aaa.bmp"));
        }
    }
}
