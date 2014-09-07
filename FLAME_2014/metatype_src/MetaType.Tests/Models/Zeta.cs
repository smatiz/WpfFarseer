using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaType.Tests.Models
{
    public class Zeta : Delta
    {
        public virtual object MissingMethod(MissingMethodCall call)
        {
            if (call.Name == "NoSuchMethod")
            {
                return "Missing method provided";
            }

            if (call.Name == "get_NoSuchProperty")
            {
                return "Missing property provided";
            }

            return call.Default();
        }
    }
}
