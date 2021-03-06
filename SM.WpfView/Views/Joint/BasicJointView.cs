﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using SM.WpfView;
using SM;

namespace SM.WpfView
{
    public abstract class BasicJointView : BasicView, IJointView
    {
        public BasicJointView(Action<CanvasId> created, IContext parent, IdInfo id)
            : base(created, parent, id)
        {
        }
    }
}
