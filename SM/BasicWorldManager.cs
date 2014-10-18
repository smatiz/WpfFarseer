using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public abstract class BasicWorldManager : BasicManager
    {
        public BasicWorldManager(IViewWatch viewWatch)
            : base(viewWatch)
        {
        }


        public void AddBodyView(IBodyView body)
        {
            AddObject(new BodyManager(body, CreateBodyMaterial()));
        }
        public abstract IBodyMaterial CreateBodyMaterial();

        public void AddBreakableBodyView(IBreakableBodyView body)
        {
            AddObject(new BreakableBodyManager(body, CreateBreakableBodyMaterial()));
        }
        public abstract IBreakableBodyMaterial CreateBreakableBodyMaterial();

    }
}
