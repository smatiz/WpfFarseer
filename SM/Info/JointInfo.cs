using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public class JointInfo : BasicInfo
    {
        public IJoint Joint { get; private set; }
        public JointInfo(IJoint joint)
        {
            Joint = joint;
        }
    }
}
