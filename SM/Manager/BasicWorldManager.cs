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


        public void __AddBodyView(__IBodyView body)
        {
            AddManager(new __BodyManager(body, __CreateBodyMaterial()));
        }

        public void AddBodyView(IBodyView body)
        {
            AddManager(new BodyManager(body, CreateBodyMaterial()));
        }
        public abstract IBodyMaterial CreateBodyMaterial();
        public abstract __IBodyMaterial __CreateBodyMaterial();

        public void AddBreakableBodyView(IBreakableBodyView body)
        {
            AddManager(new BreakableBodyManager(body, CreateBreakableBodyMaterial()));
        }
        public abstract IBreakableBodyMaterial CreateBreakableBodyMaterial();

        public void AddRopeJointControl(IRopeJointView body)
        {
            AddManager(new RopeJointManager(body, CreateRopeJointMaterial()));
        }
        public abstract IRopeJointMaterial CreateRopeJointMaterial();
    }
}
