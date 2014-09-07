using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaType.Tests.Models
{
    class Gamma : Beta
    {
        public Gamma(int foo) : base(foo) { }

        public virtual string Bar { get; set; }
    }
}
