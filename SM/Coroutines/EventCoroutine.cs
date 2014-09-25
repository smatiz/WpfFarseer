using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public class EventCoroutine : BasicCoroutine
    {
        public event Func<IEnumerator<BasicCoroutine>> Event;

        protected override IEnumerator<BasicCoroutine> DoIt()
        {
            return Event();
        }
    }
}
