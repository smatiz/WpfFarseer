using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApplication1
{
    
    public class Cross : Canvas
    {
        public Cross()
        {
            Background = new SolidColorBrush(Colors.CadetBlue);
            Width = 30;
            Height = 30;
        }

        public int ABC { get; set; }
    }
}
