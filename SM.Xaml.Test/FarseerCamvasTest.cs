using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace SM.Xaml.Test
{

    [ContentPropertyAttribute("FarseerContent")]
    public class FarseerCanvasTest : Canvas
    {
        public SM.Xaml.BasicContainer FarseerContent
        {
            get { return (SM.Xaml.BasicContainer)GetValue(FarseerContentProperty); }
            set { SetValue(FarseerContentProperty, value); }
        }
        public static readonly DependencyProperty FarseerContentProperty =
            DependencyProperty.Register("FarseerContent", typeof(SM.Xaml.BasicContainer), typeof(FarseerCanvasTest), new PropertyMetadata(null));
    }
}
