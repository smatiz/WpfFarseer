using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Wpf
{
    public class RootControl : BasicControl
    {
        public RootControl(CanvasId canvasId)
            : base(canvasId)
        {
        }

        public new void AddChild(BasicControl basic)
        {
            base.AddChild(basic);
        }
    }
}
