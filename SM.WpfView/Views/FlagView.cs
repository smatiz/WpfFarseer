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
    public partial class FlagView : BasicView
    {
        public float2 P { get; set; }
        
        CrossControl _crossControl;
        public FlagView(BasicView parent, string id)
            : base(parent, id)
        {
            _crossControl = new SM.WpfView.CrossControl();
            AddChild(_crossControl);
        }

        public override void Update()
        {
            Canvas.SetLeft(_crossControl, P.X * Context.Zoom);
            Canvas.SetTop(_crossControl, P.Y * Context.Zoom);
        }
    }
}
