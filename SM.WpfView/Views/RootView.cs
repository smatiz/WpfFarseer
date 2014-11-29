using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SM.WpfView
{
    public sealed class RootView : BasicView
    {
        IContext _context;
        public override IContext Context { get { return _context; } }
        public RootView(IContext context, Canvas parentCanvas)
            : base(parentCanvas)
        {
            _context = context;
        }

        public override void Update()
        {
            throw new NotImplementedException();
        }
    }
}
