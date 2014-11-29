using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public interface IFlag : IDescriptor
    {
        float X { get; }
        float Y { get; }
    }
}
