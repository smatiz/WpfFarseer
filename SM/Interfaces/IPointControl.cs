using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public interface IPointControl : IVector2Control
    {
        string ParentId { get; }
    }
}
