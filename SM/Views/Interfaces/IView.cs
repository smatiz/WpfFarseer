using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public interface IView
    {
        string Id { get; }
        void Update();
        List<IFlagView> Flags { get; }
    }
}
