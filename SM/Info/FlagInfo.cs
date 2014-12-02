using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public class FlagInfo : BasicInfo
    {
        public string ParentId { get; private set; }
        public string Id { get; private set; }
        public float2 P { get; private set; }
        public FlagInfo(IFlag flag, string parentId)
            : base(flag)
        {
            P = new float2(flag.X, flag.Y);
            Id = flag.Id;
            ParentId = parentId;
        }
    }
}
