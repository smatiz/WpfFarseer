using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SM
{
    public class BasicShapeView
    {
        public IContext Context { get; private set; }
        public BasicShapeView(IContext context)
        {
            Context = context;
        }
    }
}