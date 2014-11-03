using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{

    public class __Views
    {
        public List<IBodyView> Bodies { get; private set; }
        public List<IJointView> Joints { get; private set; }

        public __Views()
        {
            Bodies = new List<IBodyView>();
            Joints = new List<IJointView>();
        }
    }

}
