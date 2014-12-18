using SM;
using SM.WpfView;
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

namespace SM.WpfView
{
    public class FlagView 
    {
        public IdInfo Id 
        { 
            get
            {
                return _flag.Id;
            }
        }
        public float2 P 
        { 
            get
            {

                return new float2(_flag.X, _flag.Y);
            }
        }

        CrossControl _crossControl;
        IContext _context;
        IFlag _flag;

        public FlagView(IContext context,Canvas parent, IFlag flag)
        {
            _flag = flag;
            _context = context;
            _crossControl = new SM.WpfView.CrossControl();
            //P = info.P;
            parent.Children.Add(_crossControl);
            Canvas.SetLeft(_crossControl, _flag.X * _context.Zoom);
            Canvas.SetTop(_crossControl, _flag.Y * _context.Zoom);
        }

        
    }
}
