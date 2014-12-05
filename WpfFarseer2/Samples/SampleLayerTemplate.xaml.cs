using SM;
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
    public partial class SampleLayerTemplate : UserControl
    {
        public SampleLayerTemplate()
        {
            InitializeComponent();
        }

        public IEnumerable<transform2d> Positions
        {
            get
            {
                return new transform2d[]
                {
                    new transform2d(0,0,0,1),
                    new transform2d(100,0,0,1),
                    new transform2d(0,100,0,1),
                };
            }
        }
    }
}
