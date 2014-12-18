using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public class FlagInfo 
    {
        public IdInfo Id { get; set; }
        public IFlag Flag { get; set; }
        //public float2 P { get; set; }


        public float2 P
        {
            get
            {
                return new float2(Flag.X, Flag.Y);
            }
        }
    }
}
