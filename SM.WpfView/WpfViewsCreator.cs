﻿using System;
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
        //Canvas _rootCanvas;

        private Action<CanvasId> _created;

        public WpfViewsCreator(Action<CanvasId> created, IContext context)
        {
            _created = created;
            _context = context;
        }

        public IBodyView CreateBody(BodyInfo body, IShapeViewCreator shapeCreator)
        {
            //d.ping("uuuuuuu"); 
            return new BodyView(_created, _context, body, shapeCreator);
        }

        public IBreakableBodyView CreateBreakableBody(BodyInfo body, IShapeViewCreator shapeCreator)
        {
            return new BreakableBodyView(_created, _context, body, shapeCreator);
        }
    }
}
