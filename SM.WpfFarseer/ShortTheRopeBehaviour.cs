using SM;
using SM.Farseer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfFarseer
{
    public class ShortTheRopeBehaviour : IFarseerBehaviourWpf
    {
        FarseerPhysics.Dynamics.Joints.RopeJoint jointC;

        public IEnumerator<BasicCoroutine> Start(FarseerWorldManager farseerWorld)
        {
            jointC = ((RopeJointUpdater)farseerWorld.Find("jointC")).Joint;
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
