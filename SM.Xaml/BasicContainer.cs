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

namespace SM.Xaml
{
    [ContentPropertyAttribute("Children")]
    public abstract class BasicContainer : BasicControl, IContainer
    {
        public BasicContainer()
        {
            Children = new List<IDescriptor>();
        }
      
        public List<IDescriptor> Children
        {
            get { return (List<IDescriptor>)GetValue(FarseerObjectsProperty); }
            set { SetValue(FarseerObjectsProperty, value); }
        }
        public static readonly DependencyProperty FarseerObjectsProperty =
            DependencyProperty.Register("Children", typeof(List<IDescriptor>), typeof(Farseer), new PropertyMetadata(null));

        //public IEnumerable<IDescriptor> Descriptors { get { return Children.Select(c => (IDescriptor)c); } }


        //public string Id
        //{
        //    get { return (string)GetValue(IdProperty); }
        //    set { SetValue(IdProperty, value); }
        //}
        //public static readonly DependencyProperty IdProperty =
        //    DependencyProperty.Register("Id", typeof(string), typeof(Farseer), new PropertyMetadata("Farseer"));
    }
}
