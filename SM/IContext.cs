using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public interface IContext
    {
        float Zoom { get; }
        int EllipseNumberOfEdges { get; }
    }

    public class Context : IContext
    {
        public float Zoom { get; private set; }
        public int EllipseNumberOfEdges { get; private set; }
        public Context(float zoom)
        {
            Zoom = zoom;
            EllipseNumberOfEdges = 10;
        }
    }
}
