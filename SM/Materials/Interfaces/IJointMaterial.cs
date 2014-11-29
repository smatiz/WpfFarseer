using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public interface IJointMaterial : IMaterial
    {
        float Breakpoint { get; set; }
        bool CollideConnected { get; set; }
        void Finalize(BasicManager basicManager);
    }
}
