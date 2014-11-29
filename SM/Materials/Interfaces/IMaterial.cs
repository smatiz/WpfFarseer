using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public interface IMaterial 
    {
        string Id { get; }
        object Object { get; }
    }
}
