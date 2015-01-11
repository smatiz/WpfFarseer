using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public class JointInfo 
    {
        public IdInfo Id { get; set; }
        public bool CollideConnected { get; set; }
    }

    public class RopeJointInfo : JointInfo
    {
        public string TargetFlagIdA { get; set; }
        public string TargetFlagIdB { get; set; }
    }
}
