using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SM.Wpf
{
    public interface IBreakableShape
    {
        Rect BBox { get; }
        IEnumerable<PointCollection> PointCollections { get; }
        float Density { get; }
    }
}
