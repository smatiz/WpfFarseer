using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SM.Wpf
{
    public interface ISkinnableCanvas
    {
        IEnumerable<IEnumerable<float2>> FindBorder(VisualBrush brush, double w, double h);
    }
}
