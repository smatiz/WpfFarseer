using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Wpf
{
    public interface IFlaggable : IIdentifiable
    {
        ObservableCollection<FlagControl> Flags { get; }
    }
}
