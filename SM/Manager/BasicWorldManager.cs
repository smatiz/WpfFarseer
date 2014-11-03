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

        public void AddBodyView(__IBodyView body)
        {
            AddManager(new __BodyManager(body, CreateBodyMaterial()));
        }
        public void AddBreakableBodyView(__IBodyView body)
        {
            AddManager(new __BreakableBodyManager(body, CreateBreakableBodyMaterial()));
        }

        public abstract __IBodyMaterial CreateBodyMaterial();
        public abstract __IBreakableBodyMaterial CreateBreakableBodyMaterial();

        public void AddRopeJointControl(IRopeJointView body)
        {
            AddManager(new RopeJointManager(body, CreateRopeJointMaterial()));
        }
        public abstract IRopeJointMaterial CreateRopeJointMaterial();
    }
}
