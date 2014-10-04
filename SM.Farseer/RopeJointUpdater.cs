using FarseerPhysics.Dynamics.Joints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SM.Farseer
{
    public class RopeJointUpdater : IFarseerObject
    {
        private IJointObject _jointControl;
        RopeJoint _joint;
        public object Object { get { return _joint; } }

        public RopeJointUpdater(IJointObject jointControl, RopeJoint j)
        {
            _jointControl = jointControl;
            _joint = j;
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
