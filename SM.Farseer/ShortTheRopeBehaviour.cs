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
    public class ShortTheRopeBehaviour : IFarseerBehaviourWpf
    {
        RopeJoint jointC;

        public IEnumerator<BasicCoroutine> Start(FarseerWorldManager farseerWorld)
        {
            jointC = farseerWorld.FindObject<RopeJoint>("jointC");
            return null;
        }

        public IEnumerator<BasicCoroutine> Update()
        {
            return null;
        }

        public IEnumerator<BasicCoroutine> Loop(float dt)
        {
            yield return new WaitSecondsCoroutine(5);
            jointC.MaxLength *= 0.4f;
        }
    }
}
