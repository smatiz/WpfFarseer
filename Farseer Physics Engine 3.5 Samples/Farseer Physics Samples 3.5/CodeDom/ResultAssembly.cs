using System;
using System.Collections.Generic;

using System.Text;
using System.Reflection;

namespace Flame.Dlr
{
    public class ResultAssembly
    {
        public Type Language { get; set; }
        public bool Loaded { get; set; }
        public Exception Exception { get; set; }
    }
}
