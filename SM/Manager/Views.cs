using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
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
