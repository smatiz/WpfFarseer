using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public interface IJointControl : IIdentifiable
    {
        bool CollideConnected { get; }
    }
}
