using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using WpfApplication1;

namespace WpfFarseerEditor.wpf
{
    public class SelectableCanvas : Canvas, ISelectableObject
    {

        public System.Windows.UIElement Movable
        {
            get { return this; }
        }

        public System.Windows.UIElement Hover
        {
            get { return this; }
        }

        public System.Windows.UIElement Selection
        {
            get { return this; }
        }

        public bool Selected
        {
            get;
            set;
        }
    }
}
