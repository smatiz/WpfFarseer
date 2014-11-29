using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public abstract class BasicWorldManager : BasicManager
    {
        public BasicWorldManager(Synchronizers synchronizers, IWatchView viewWatch)
            : base(synchronizers,viewWatch)
        {
        }

        //public void AddBodyView(IBodyView body)
        //{
        //    if (body.BodyType == SM.BodyType.Breakable)
        //        AddManager(new BreakableBodySynchronizer(body, CreateBreakableBodyMaterial()));
        //    else
        //        AddManager(new BodySynchronizer(body, CreateBodyMaterial()));
        //}

        //public abstract IBodyMaterial CreateBodyMaterial();
        //public abstract IBreakableBodyMaterial CreateBreakableBodyMaterial();

        //public void AddRopeJointControl(IRopeJointView body)
        //{
        //    AddManager(new RopeJointSynchronizer(body, CreateRopeJointMaterial()));
        //}
        //public abstract IRopeJointMaterial CreateRopeJointMaterial();
    }
}
