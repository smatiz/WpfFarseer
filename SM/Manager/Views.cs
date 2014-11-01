using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{

    public class __Views
    {
        public List<IBreakableBodyView> BreakableBodies { get; private set; }
        public List<__IBodyView> Bodies { get; private set; }
        public List<IJointView> Joints { get; private set; }

        public __Views()
        {
            BreakableBodies = new List<IBreakableBodyView>();
            Bodies = new List<__IBodyView>();
            Joints = new List<IJointView>();
        }
    }

    public class Views
    {
        public List<IBreakableBodyView> BreakableBodies { get; private set; }
        public List<IBodyView> Bodies { get; private set; }
        public List<IJointView> Joints { get; private set; }

        public Views()
        {
            BreakableBodies = new List<IBreakableBodyView>();
            Bodies = new List<IBodyView>();
            Joints = new List<IJointView>();
        }
    }
}
