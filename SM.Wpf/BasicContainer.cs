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
    public abstract class BasicContainer : Panel, IContainer
    {

        static IEnumerable<IDescriptor> GetAllDescriptors(BasicContainer container)
        {
            var childrenLinq = container.Children.Cast<object>();
            var descriptors = childrenLinq.Where(x => x is IDescriptor).Select(x => (IDescriptor)x);
            foreach (var c in childrenLinq.Where(x => x is BasicContainer).Select(x => (BasicContainer)x))
            {
              descriptors =  descriptors.Concat(GetAllDescriptors(c));
            }
            return descriptors;
        }

        public IEnumerable<IDescriptor> Descriptors
        {
            get
            {
                return GetAllDescriptors(this);
            }
        }

        public string Id { get; set; }
    }
}
