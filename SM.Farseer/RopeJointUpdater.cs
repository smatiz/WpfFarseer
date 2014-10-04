using FarseerPhysics.Dynamics.Joints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SM.Farseer
{
    public class RopeJointUpdater : IUpdatable
    {
        private IJointObject _jointControl;
        public RopeJoint Joint { get; private set; }

        public RopeJointUpdater(IJointObject jointControl, RopeJoint j)
        {
            _jointControl = jointControl;
            Joint = j;
        }

        public void Update()
        {
        }

        public string Id
        {
            get 
            { 
                return _jointControl.Id;
            }
        }
    }
}
