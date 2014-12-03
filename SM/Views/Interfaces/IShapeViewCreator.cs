using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public interface IShapeViewCreator
    {
        IEnumerable<BasicShapeView> Create(IEnumerable<IShape> shape, IContext context, float scale);
    }
}
