using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public class EmptyCoroutine : BasicCoroutine
    {
        protected override IEnumerator<BasicCoroutine> DoIt()
        {
            yield break;
        }
    }
}
