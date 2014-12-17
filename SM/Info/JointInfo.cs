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
        //public IJoint Joint { get; set; }
    }

    public class RopeJointInfo : JointInfo
    {
        public FlagInfo FlagA { get; set; }
        public FlagInfo FlagB { get; set; }
        
    }
}
