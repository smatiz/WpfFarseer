using SM;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Threading;
using System.Collections.ObjectModel;
using System.Windows.Markup;
using SM.Wpf;
using SM.WpfView;

namespace SM.Xaml
{
    [ContentPropertyAttribute("Children")]
    public class User : UserControl, IContainer
    {
        public User()
        {
            Children = new List<BasicControl>();
        }


        public List<BasicControl> Children
        {
            get { return (List<BasicControl>)GetValue(FarseerObjectsProperty); }
            set { SetValue(FarseerObjectsProperty, value); }
        }
        public static readonly DependencyProperty FarseerObjectsProperty =
            DependencyProperty.Register("Children", typeof(List<BasicControl>), typeof(User), new PropertyMetadata(null));

        public IEnumerable<IDescriptor> Descriptors { get { return Children.Select(c => (IDescriptor)c); } }

       
      
    }
}
