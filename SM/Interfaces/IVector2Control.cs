using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public interface IVector2Control : IIdentifiable, IVector2
    {
    }

    public interface IVector2
    {
        float X { get; }
        float Y { get; }
    }

    public struct float2 : IVector2
    {
        public float X { get; set; }
        public float Y { get; set; }
    }
}
