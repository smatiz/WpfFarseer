using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public interface IManager : IIdentifiable
    {
        void UpdateMaterial();
        void UpdateView();
        void Build();
        object Object { get; }
    }
}
