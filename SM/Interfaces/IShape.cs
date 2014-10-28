using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public interface IShape
    {
        IEnumerable<float2> Points_X { get; }
        float Density_X { get; }
    }
}
