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
    public class ShortTheRopeBehaviour : IBehaviourView
    {
        RopeJoint _jointC;

        public IEnumerator<BasicCoroutine> Start(BasicManager farseerWorld)
        {
            return null;
        }

        public IEnumerator<BasicCoroutine> Update()
        {
            yield return new WaitSecondsCoroutine(5);
            _jointC.MaxLength *= 0.4f;
        }


        public ShortTheRopeBehaviour(RopeJoint jointC)
        {
            _jointC = jointC;
        }
    }
}
