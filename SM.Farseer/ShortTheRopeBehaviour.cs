using FarseerPhysics.Dynamics.Joints;
using SM;
using SM.Farseer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Farseer
{
    public class ShortTheRopeBehaviour : IViewBehaviour
    {
        RopeJoint jointC;

        public IEnumerator<BasicCoroutine> Start(BasicManager farseerWorld)
        {
            jointC = farseerWorld.FindObject<RopeJoint>("jointC");
            return null;
        }

        public IEnumerator<BasicCoroutine> Update()
        {
            yield return new WaitSecondsCoroutine(5);
            jointC.MaxLength *= 0.4f;
        }

    }
}
