using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public abstract class BasicWorldManager : BasicManager
    {
        public BasicWorldManager(Synchronizers synchronizers, Info info, IWatchView viewWatch)
            : base(synchronizers, info, viewWatch)
        {
        }
    }
}
