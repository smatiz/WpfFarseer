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
            if (body.BodyType == SM.BodyType.Breakable)
                AddManager(new BreakableBodyManager(body, CreateBreakableBodyMaterial()));
            else
                AddManager(new BodyManager(body, CreateBodyMaterial()));
        }

        public abstract IBodyMaterial CreateBodyMaterial();
        public abstract IBreakableBodyMaterial CreateBreakableBodyMaterial();

        public void AddRopeJointControl(IRopeJointView body)
        {
            AddManager(new RopeJointManager(body, CreateRopeJointMaterial()));
        }
        public abstract IRopeJointMaterial CreateRopeJointMaterial();
    }
}
