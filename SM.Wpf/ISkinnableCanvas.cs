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
    public interface __ISkinnableCanvas
    {
        IEnumerable<IEnumerable<float2>> __FindBorder(VisualBrush brush, double w, double h);
    }


    public interface ISkinnableCanvas
    {
        List<PointCollection> FindBorder(VisualBrush brush, double w, double h, int n);
    }
}
