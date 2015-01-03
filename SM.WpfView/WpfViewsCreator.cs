using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SM.WpfView
{
    public partial class WpfViewsCreator : IViewCreator
    {
        IContext _context;
        Canvas _rootCanvas;
        public WpfViewsCreator(Canvas rootCanvas, IContext context)
        {
            _rootCanvas = rootCanvas;
            _context = context;
        }

        public IBodyView CreateBody(BodyInfo body, IShapeViewCreator shapeCreator)
        {
            return new BodyView(_rootCanvas, _context, body, shapeCreator);
        }

        public IBreakableBodyView CreateBreakableBody(BodyInfo body, IShapeViewCreator shapeCreator)
        {
            return new BreakableBodyView(_rootCanvas, _context, body, shapeCreator);
        }
    }
}
