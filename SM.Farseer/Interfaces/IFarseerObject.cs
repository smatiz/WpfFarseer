using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Farseer
{
    internal interface IFarseerObject : IFarseerId
    {
        object Object { get; }
    }
}
